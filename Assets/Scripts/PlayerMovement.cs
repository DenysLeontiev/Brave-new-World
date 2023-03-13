using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float sphereCastYPosition = 0.85f;
    [SerializeField] private LayerMask groundMask;
    Vector3 spherePos;
    private float groundYOffset = 0.7f;

    [SerializeField] private float jumpForce = 15f;

    [HideInInspector] public float moveSpeed = 1.0f;
    public float walkSpeed = 4.0f;
    public float runSpeed = 10.0f;


    private CharacterController characterController;
    private PlayerAnimations playerAnimations;

    [HideInInspector] public Animator playerAnimator;

    [SerializeField] private GameObject playerFollowCamera;

    [HideInInspector] public Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    #region PlayerRotateRegion
    [SerializeField] private float smoothTime = 0.1f;
    private float currentVelocity;
    #endregion

    #region GravityRegion
    private Vector3 gravityVelocity; // used to handle gravity
    [SerializeField] float gravityScale = -9.81f;
    #endregion

    #region MovementStatesRegion
    private BaseMovementState currentState;
    [HideInInspector] public WalkState Walk= new WalkState();
    [HideInInspector] public RunState Run = new RunState();
    #endregion

    private void Start()
    {
        characterController = GetComponent<CharacterController>();  
        playerAnimator = GetComponentInChildren<Animator>();
        playerAnimations = GetComponentInChildren<PlayerAnimations>();
        SwitchState(Walk);
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();
        HandleJumping();
        HandleMovementAnimation();

        currentState.UpdateState(this);
    }


    public void SwitchState(BaseMovementState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void HandleMovementAnimation()
    {
        playerAnimator.SetFloat("horizontalInput", horizontalInput);
        playerAnimator.SetFloat("verticalInput", verticalInput);
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + playerFollowCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleJumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("I am here");
            playerAnimations.Jump();
            gravityVelocity.y += jumpForce;
        }
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if(Physics.CheckSphere(spherePos, characterController.radius - 0.05f, groundMask))
        {
            return true;
        }
        return false;
    }

    private void HandleGravity()
    {
        if (!IsGrounded())
        {
            gravityVelocity.y += gravityScale * Time.deltaTime;
        }
        else if(gravityVelocity.y < 0)
        {
            gravityVelocity.y = -15f;
        }

        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (characterController != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(spherePos, characterController.radius - 0.05f);
        }
    }
}

