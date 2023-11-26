using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 walkDirectionVector = Vector2.left;
    public enum WalkableDirection
    {
        Right,
        Left
    }
    private WalkableDirection _walkDirection = WalkableDirection.Left;
    public WalkableDirection WalkDirection 
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if(_walkDirection != value)
            {
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }    
    }


    private bool _hasTarget = false;
    public bool HasTarget { 
        get { return _hasTarget; } 
        private set 
        { 
            _hasTarget = value;
            animator.SetBool(AnimationStates.hasTarget, value);
        } 
    }

    public bool CanMove => animator.GetBool(AnimationStates.canMove);


    public TouchPointDirections touchPointDirections;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public float walkStopRate = 0.5f;


    public Damageable damageable;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Update()
    {
        HasTarget = attackZone.detectedCollider.IsUnityNull() ? false : true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchPointDirections.IsGrounded && (touchPointDirections.IsOnWall || cliffDetectionZone.detectedCollider is null))
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && touchPointDirections.IsGrounded)
                rb.velocity = new Vector2(walkDirectionVector.x * moveSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left) 
        { 
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Walable enemy direction is not set to legal values");
        }
        gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        //rb.AddForce(knockback);
        //print("Hit velocity " + rb.velocity);
    }
    
    public void FacePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(transform.position.x < player.transform.position.x && WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
            gameObject.transform.localScale = new Vector2(Math.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }            
        else if(transform.position.x > player.transform.position.x && WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
            
    }

}
