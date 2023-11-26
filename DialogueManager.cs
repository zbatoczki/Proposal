using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public float typingWaitTimeInSeconds;
    private Queue<string> sentences = new Queue<string>();
    public TMP_Text sentenceWindow;
    public Button continueBtn;
    public Animator dialogueAnimator;
    public Animator enemyAnimator;
    public PlayerInput playerInput;
    public AudioSource textSfx;
    public ArrowLauncher arrowLauncher;
    public MusicPlayer musicPlayer;
    public Enemy enemy;
    public bool goToQustion;
    internal void StartDialog(Dialogue dialogue)
    {
        playerInput.actions.FindActionMap("Player").Disable();
        enemyAnimator.SetBool(AnimationStates.canMove, false);
        enemyAnimator.SetBool(AnimationStates.dialogueOpen, true);
        arrowLauncher.canFire = false;
        arrowLauncher.dialogueOpen = true;
        arrowLauncher.DestroyAllArrows();
        dialogueAnimator.SetBool(AnimationStates.isOpen, true);
        sentences.Clear();
        foreach(string sentence in dialogue.dialogues)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        enemy.FacePlayer();
        if (sentences.Count != 0)
        {
            textSfx.Play();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences.Dequeue()));
        }
        else if(!goToQustion)
        {
            EndDialogue();
        }
        else
        {
            Invoke( "GoToQuestion", 1f);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceWindow.text = "";
        foreach(char c in sentence)
        {
            sentenceWindow.text += c;
            yield return new WaitForSeconds(typingWaitTimeInSeconds);
        }
        textSfx.Stop();
    }

    public void EndDialogue()
    {
        textSfx.Stop();
        playerInput.actions.FindActionMap("Player").Enable();
        enemyAnimator.SetBool(AnimationStates.canMove, true);
        enemyAnimator.SetBool(AnimationStates.dialogueOpen, false);
        dialogueAnimator.SetBool(AnimationStates.isOpen, false);
        arrowLauncher.dialogueOpen = false;
    }

    public void GoToQuestion()
    {
        SceneManager.LoadScene("Question");
    }
}
