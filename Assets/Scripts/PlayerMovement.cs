using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string ISWALKING_ANIM_PARAMETER = "isWalking";

    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform gfxTransform;
    [SerializeField] private Animator animator;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float groundCheckRadius = 2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private Vector2 movementInput;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

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

        Vector3 moveVector = moveDir * speed * 10f * Time.deltaTime;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

        float rotateSpeed = 10f;
        gfxTransform.forward = Vector3.Slerp(gfxTransform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundLayer);
    }

    private void UpdateAnimations()
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
