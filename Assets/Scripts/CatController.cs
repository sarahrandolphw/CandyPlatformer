using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore;

// Make sure RigidBody exists
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class CatController : MonoBehaviour
{
    public float walkSpeed = 7f;
    [SerializeField]
    private bool _isJumping = false;
    Vector2 moveInput;
    public float jumpImpulse = 9.5f;
    public float airWalkSpeed = 7f;
    TouchingDirections touchingDirections;
    public bool _isFacingRight = true;
    public bool IsMoving { get; private set; }
    public bool IsJumping {get
        {
            return _isJumping;
        } private set
        {
            _isJumping = value;
            // animator.SetBool(AnimationStrings.isJumping, value);
        }
    }
    public float CurrentMoveSpeed{ get
        {
            if(!touchingDirections.IsOnWall || touchingDirections.IsOnWall){
                if(touchingDirections.IsGrounded){
                    return walkSpeed;
                }else
                {
                    return airWalkSpeed;
                }
                
            } else {
                return 0;
            }
        }}

    public bool IsFacingRight { get { return _isFacingRight; } private set {
            if(_isFacingRight != value) {
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    // found as soon as possible
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        // Time.fixedDeltaTime
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // face right
            IsFacingRight = true;
        }else if(moveInput.x < 0 && IsFacingRight)
        {
            // face left
            IsFacingRight = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            // Debug.Log("OnJump called");
            // Debug.Log(animator != null ? "Animator assigned" : "Animator is null");
            animator.SetBool("Jump", true);
            StartCoroutine(ResetJumpTrigger());
        }
    }
    private IEnumerator ResetJumpTrigger()
{
    // Wait for the end of the frame to ensure the Animator reads the true value first
    yield return new WaitForSeconds(0.1f);
    animator.SetBool("Jump", false);
}
    // Might implement later
    // public void OnRun(InputAction.CallbackContext context)
    // {
    //     if(context.started)
    //     {

    //     }
    // }
    
}
