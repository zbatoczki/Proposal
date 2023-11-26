using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public bool enableDebugInvincibility;
    public UnityEvent<int, Vector2> damagableHit;
    private Dialogue proposalDialogue;
    public UnityEvent<Dialogue> triggerDialogue;
    public UnityEvent<int,int> healthChanged;
    public DetectionZone attackZone;
    public MusicPlayer player;
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth = 100;
    public DialogueManager dialogueManager;
    public int MaxHealth {
        get { return _maxHealth; }
        set { _maxHealth = value; } 
    }
    public int CurrentHealth {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if(_currentHealth <= 0)
            {
                _currentHealth = 0;
                IsAlive = false;
                if (gameObject.tag == "Enemy")
                {
                    attackZone.ClearDetectionCollider();
                    gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
                    animator.SetBool(AnimationStates.hasTarget, false);
                    attackZone.enabled = false;
                    dialogueManager.goToQustion = true;
                    triggerDialogue?.Invoke(proposalDialogue);
                    player.StopPlaying();
                }                   
            }
            healthChanged?.Invoke(_currentHealth, MaxHealth);

        }
    }

    private bool _isAlive = true;
    public bool IsAlive {
        get { return _isAlive; }
        set 
        { 
            _isAlive = value;
            animator.SetBool(AnimationStates.isAlive, value);
        }
    }

    [SerializeField]
    private bool _isInvincible = false;
    public bool IsInvincible { 
        get { return _isInvincible; } 
        set { _isInvincible = value; } 
    }


    public Animator animator;

    private float timeSinceHit = 0;
    public float invincibilityTimeLimit = 1;

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStates.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStates.lockVelocity, value);
        }
    }

    private void Awake()
    {
        proposalDialogue = GameObject.Find("EnemyDialogue").GetComponent<DialogueTrigger>().dialogue2;
    }

    private void Update()
    {
        UpdateInvincibility();
    }

    private void UpdateInvincibility()
    {
        if (IsInvincible)
        {
            if (timeSinceHit > invincibilityTimeLimit)
            {
                IsInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        bool hitSuccessful = false;
        if (IsAlive && !IsInvincible)
        {
            if(!enableDebugInvincibility)
                CurrentHealth -= damage;
            IsInvincible = true;
            hitSuccessful = true;
            LockVelocity = true;
            animator.SetTrigger(AnimationStates.hitTrigger);
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }
        return hitSuccessful;
    }
}
