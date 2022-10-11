using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementV2 : MonoBehaviour {
    public enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    [Header("Components")]
    private Rigidbody2D rb2d;

    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private PlayerMovementState movementState = PlayerMovementState.Walking;
    public Animator animator;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    [SerializeField] private float sneakingSpeedDivider = 2f;
    [SerializeField] private float runningSpeedMultiplier = 2.5f;
    private float horizontalDirection;
    private float verticalDirection;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
    }

    private void Update() {
        horizontalDirection = GetInput().x;
        verticalDirection = GetInput().y;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection) + Mathf.Abs(verticalDirection));
    }

    private void FixedUpdate() {
        SwitchWalkingState();
        MoveCharacter();
        fieldOfView.SetOrigin(transform.position);
        rb2d.drag = linearDrag;

    }

    // Get input controllers
    private static Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // TODO: UPDATE TO THE NEW INPUT SYSTEM
    private void SwitchWalkingState() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            movementState = PlayerMovementState.Running;
            animator.SetInteger("Movement", 3);
        } else if (Input.GetKey(KeyCode.Space)) {
            movementState = PlayerMovementState.Sneaking;
            animator.SetInteger("Movement", 1);
        } else {
            movementState = PlayerMovementState.Walking;
            animator.SetInteger("Movement", 2);
        }
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
