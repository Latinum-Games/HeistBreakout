using System;
using UnityEngine;

public class MovementV2 : MonoBehaviour {
    public enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    [Header("Components")]
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private PlayerMovementState movementState;
    private Rigidbody2D rb2d;
    public Animator animator;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    [SerializeField] private float sneakingSpeedDivider = 2f;
    [SerializeField] private float runningSpeedMultiplier = 2.5f;
    private Vector2 inputVector;
    private float horizontalDirection;
    private float verticalDirection;

    private void Start() {
        inputVector = Vector2.zero;
        movementState = PlayerMovementState.Walking;
        animator.SetInteger("Movement", 2);
        
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
    }

    private void Update() {
        horizontalDirection = inputVector.x;
        verticalDirection = inputVector.y;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection) + Mathf.Abs(verticalDirection));
    }

    private void FixedUpdate() {
        MoveCharacter();
        fieldOfView.SetOrigin(transform.position);
        rb2d.drag = linearDrag;

    }

    private void MoveCharacter() {
        var velocity = rb2d.velocity;
        rb2d.AddForce(new Vector2(horizontalDirection, verticalDirection) * movementAcceleration);

        // TODO ADD THE MOVEMENT ACCELERATION TO THE MOVEMENT SPEED CALCULATIONS FOR THE STATES
        var currentMaxMovementSpeed = movementState switch { 
            PlayerMovementState.Sneaking => baseMovementSpeed / sneakingSpeedDivider,
            PlayerMovementState.Walking => baseMovementSpeed,
            PlayerMovementState.Running => baseMovementSpeed * runningSpeedMultiplier,
            _ => throw new ArgumentOutOfRangeException()
        };

        // Max Speed on X Axis
        if (Mathf.Abs(velocity.x) > currentMaxMovementSpeed) {
            rb2d.velocity = new Vector2(Mathf.Sign(velocity.x) * currentMaxMovementSpeed, velocity.y).normalized;
        }

        // Max Speed on Y Axis
        if (Mathf.Abs(velocity.y) > currentMaxMovementSpeed) {
            rb2d.velocity = new Vector2(velocity.x, Mathf.Sign(velocity.y) * currentMaxMovementSpeed).normalized;
        }
    }

    // Public functions
    public void SetInput(Vector2 vector2) {
        inputVector = vector2;
    }

    public void SetMovementState(int state) {
        switch (state) {
            case 1: // 0 For Sneaking State
                movementState = PlayerMovementState.Sneaking;
                animator.SetInteger("Movement", 1);
                break;
            case 2: // 1 For Running State
                movementState = PlayerMovementState.Walking;
                animator.SetInteger("Movement", 2);
                break;
            case 3:
                movementState = PlayerMovementState.Running;
                animator.SetInteger("Movement", 3);
                break;
        }
    }
    
    public PlayerMovementState GetPlayerMovementState() {
        return movementState;
    }

    public float GetHorizontalDirection() {
        return horizontalDirection;
    }

    public float GetVerticalDirection() {
        return verticalDirection;
    }
}
