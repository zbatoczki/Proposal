using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    private float timeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    private Color startColor;
    GameObject gameObjectToRemove;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        gameObjectToRemove = animator.gameObject;
        spriteRenderer = gameObjectToRemove.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 1)
        {
            spriteRenderer.color = new Color(255,255,255);
            timeElapsed += Time.deltaTime;
            float newAlpha = startColor.a * (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            if (timeElapsed >= fadeTime) 
            { 
                Destroy(gameObjectToRemove);
            }
        }
        
    }

}
