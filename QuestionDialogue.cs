using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestionDialogue : MonoBehaviour
{
    public float typingWaitTimeInSeconds;
    private Queue<string> sentences = new Queue<string>();
    public AudioSource textSfx;
    public TMP_Text textTfx;

    public Dialogue dialogue;


    private void Start()
    {
        StartDialog(dialogue);
    }

    internal void StartDialog(Dialogue dialogue)
    {
        foreach (string sentence in dialogue.dialogues)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count != 0)
        {
            textSfx.Play();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences.Dequeue()));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        textTfx.text = "";
        foreach (char c in sentence)
        {
            textTfx.text += c;
            yield return new WaitForSeconds(typingWaitTimeInSeconds);
        }
        textSfx.Stop();
    }
}
