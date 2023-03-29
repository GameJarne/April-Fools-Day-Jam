using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const string ISWALKING_ANIM_PARAMETER = "isWalking";

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private Animator animator;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float groundCheckRadius = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AnimationClip startJumpAnim;

    private Rigidbody rb;
    private PlayerInput playerInput;

    private float currentSpeed;
    private Vector2 movementInput;

    private bool isMoving;
    private bool isJumping = false;
    private float timeSinceJumped = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnJumpAction += PlayerInput_OnJumpAction;
        currentSpeed = walkSpeed;
    }

    private void PlayerInput_OnJumpAction(object sender, System.EventArgs e)
    {
        if (IsGrounded())
        {
            Jump();
        }
    }

    private void Update()
    {
        movementInput = playerInput.GetMovementVectorNormalized();
        currentSpeed = (Input.GetKey(KeyCode.LeftShift) && IsGrounded()) ? runSpeed : walkSpeed;

        if (isJumping)
        {
            timeSinceJumped += Time.deltaTime;

            if (timeSinceJumped >= startJumpAnim.length)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }
        }
        else
            animator.SetBool("inAir", !IsGrounded());

        if (movementInput.x != 0 || movementInput.y != 0)
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0f, movementInput.y);

        Vector3 moveVector = moveDir * currentSpeed * 10f * Time.deltaTime;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        float rotateSpeed = 10f;
        gfxTransform.forward = Vector3.Slerp(gfxTransform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void Jump()
    {
        timeSinceJumped = 0f;
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);
    }

    private void UpdateAnimations()
    {
        if (!isJumping)
        {
            if (isMoving)
            {
                animator.SetBool(ISWALKING_ANIM_PARAMETER, true);
            }
            else
            {
                animator.SetBool(ISWALKING_ANIM_PARAMETER, false);
            }
        }
    }
}
