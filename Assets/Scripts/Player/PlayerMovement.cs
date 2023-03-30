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

    private bool allowMoving = true;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private PlayerHealth playerHealth;

    private bool isSprinting;
    private float currentSpeed;
    private Vector2 movementInput;

    private bool isMoving;
    private bool isJumping = false;
    private float timeSinceJumped = 0f;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        playerInput.OnJumpAction.AddListener(OnJumpAction);

        playerInput.OnSprintPerformed.AddListener(OnSprintKeyDown);
        playerInput.OnSprintCanceled.AddListener(OnSprintKeyUp);

        playerHealth.onDeath.AddListener(Die);

        currentSpeed = walkSpeed;
        isSprinting = false;

        allowMoving = true;
    }

    private void Die()
    {
        allowMoving = false;

        this.enabled = false;
    }

    private void OnSprintKeyDown()
    {
        isSprinting = true;
    }
    private void OnSprintKeyUp()
    {
        isSprinting = false;
    }


    private void OnJumpAction()
    {
        if (!allowMoving)
            return;

        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            Jump();
        }
    }

    private void Update()
    {
        if (!allowMoving)
            return;

        isGrounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer); ;

        movementInput = playerInput.GetMovementVectorNormalized();
        currentSpeed = (isSprinting && isGrounded) ? runSpeed : walkSpeed;

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
            animator.SetBool("inAir", !isGrounded);

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
        if (!allowMoving)
            return;

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
