using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue dialogue2;

    public void TriggerDialogue(Dialogue d)
    {
        FindObjectOfType<DialogueManager>().StartDialog(d);
    }



    private void Start()
    {
        TriggerDialogue(dialogue);
    }
}
