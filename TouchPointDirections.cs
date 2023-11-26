using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchPointDirections))]
public class TouchPointDirections : MonoBehaviour
{
    public Rigidbody2D rb;

    public Collider2D touchCollider;
    public ContactFilter2D groundCastFilter;
    public ContactFilter2D wallCastFilter;
    public ContactFilter2D ceilingCastFilter;

    public float groundDistance = 0.05f;
    public float wallDistance = 0.05f;
    public float ceilingDistance = 0.02f;

    RaycastHit2D[] groundHits = new RaycastHit2D [1];
    RaycastHit2D[] wallHits = new RaycastHit2D[1];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[1];


    public Animator animator;

    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded { 
        get 
        { 
            return _isGrounded; 
        } 
        private set 
        { 
            _isGrounded=value;
            animator.SetBool(AnimationStates.isGrounded, value);
        } 
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStates.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _ceilingHit = false;
    public bool CeilingHit { 
        get
        {
            return _ceilingHit;
        }
        set 
        { 
            _ceilingHit=value;
            animator.SetBool(AnimationStates.ceilingHit, value);
        } 
    }

    private Vector2 wallDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchCollider.Cast(Vector2.down, groundCastFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchCollider.Cast(wallDirection, wallCastFilter, wallHits, wallDistance) > 0;
        CeilingHit = touchCollider.Cast(Vector2.up, ceilingCastFilter, ceilingHits, ceilingDistance) > 0;
    }
}
