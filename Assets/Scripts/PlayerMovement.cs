using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float sphereCastYPosition = 0.85f;


    [HideInInspector] public float moveSpeed = 1.0f;
    public float walkSpeed = 4.0f;
    public float runSpeed = 10.0f;


    private CharacterController characterController;

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
        SwitchState(Walk);
    }

    private void Update()
    {
        HandleGravity();
        HandleMovement();
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

    private bool IsGrounded()
    {
        Vector3 castPosition = new Vector3(transform.position.x, transform.position.y - sphereCastYPosition, transform.position.z);
        float radius = 0.2f;
        if (!Physics.SphereCast(castPosition,radius, Vector3.down, out RaycastHit hit))
        {
            return true;
        }
        return false;
    }

    private void HandleGravity()
    {
        if (IsGrounded())
        {
            float initialGravity = -15f;
            gravityVelocity.y = initialGravity;
        }
        else
        {
            gravityVelocity.y += gravityScale;
        }

        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if(characterController == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - sphereCastYPosition, transform.position.z), 0.2f);
    }
}

