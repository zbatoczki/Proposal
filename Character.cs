using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchPointDirections))]
public class Character : MonoBehaviour
{
    public Rigidbody2D rb;
    public float walkSpeed = 5f;
    public float jumpSpeed = 5f;
    private Vector2 movementInput;

    #region ANIMATION
    public Animator animator;

    public bool CanMove => animator.GetBool(AnimationStates.canMove);


    public bool IsMoving { 
        get { 
            return _isMoving; 
        } 
        set { 
            _isMoving = value;
            animator.SetBool(AnimationStates.isMoving, value);
        } 
    }
    private bool _isMoving;
    private bool _isFacingRight = true;

    public PlayerAttackHitBox attackHitBox;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }
    #endregion

    public TouchPointDirections touchPointDirections;
    public float jumpImpulse;


    public float fallMultiplier = 1f;
    public float lowJumpMultiplier = 1.5f;
    private float downVelocityAddition;

    public bool IsAlive => animator.GetBool(AnimationStates.isAlive);

    public Damageable damageable;

    // Start is called before the first frame update
    void Start()
    {
        downVelocityAddition = Physics2D.gravity.y * fallMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(CanMove)
            MoveCharacter();   
        else
            movementInput = Vector2.zero;
    }

    #region MOVEMENT
    public void OnMove(CallbackContext cbc) 
    {
        
        if (IsAlive)
        {
            movementInput = cbc.ReadValue<Vector2>();
            IsMoving = movementInput != Vector2.zero;
            SetFacingDirection(movementInput);
        }
        else
        {
            IsMoving = false;
            movementInput = Vector2.zero;
        }     
    }

    private void SetFacingDirection(Vector2 movementInput)
    {
        if(movementInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
        else if(movementInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
    }

    private void MoveCharacter()
    {
        float yVelocity = SetYVelocity();      
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(movementInput.x * walkSpeed, yVelocity);
        animator.SetFloat(AnimationStates.yVelocity, rb.velocity.y);
    }

    private float SetYVelocity()
    {
        float yVelocity = rb.velocity.y;
        if (yVelocity < 0)
        {
            yVelocity += downVelocityAddition * Time.deltaTime;
        }
        return yVelocity;
    }

   public void OnJump(CallbackContext cbc)
    {
        if(cbc.started && touchPointDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStates.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    
    #endregion
    public void OnAttack(CallbackContext cbc)
    {
        if (cbc.started)
        {
            animator.SetTrigger(AnimationStates.attack);
            //sFXPlayer.SwingSword();
        }
    }

    public void StartAttack()
    {
        attackHitBox.Attack();
    }
    public void StopAttack()
    {
        attackHitBox.StopAttack();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }


}
