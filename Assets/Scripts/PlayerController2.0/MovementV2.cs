using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementV2 : MonoBehaviour {

    public Animator animator;

    public enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    [Header("Components")]
    private Rigidbody2D _rb2d;

    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private PlayerMovementState movementState = PlayerMovementState.Walking;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    [SerializeField] private float sneakingSpeedMultiplier = 0.33f;
    [SerializeField] private float runningSpeedMultiplier = 2.5f;
    private float _horizontalDirection;
    private float _verticalDirection;

    private void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.drag = linearDrag;
    }

    private void Update() {
        _horizontalDirection = GetInput().x;
        _verticalDirection = GetInput().y;
        animator.SetFloat("Speed", Mathf.Abs(_horizontalDirection) + Mathf.Abs(_verticalDirection));
    }

    private void FixedUpdate() {
        SwitchWalkingState();
        MoveCharacter();
        fieldOfView.SetOrigin(transform.position);
        _rb2d.drag = linearDrag;

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
        var velocity = _rb2d.velocity;
        _rb2d.AddForce(new Vector2(_horizontalDirection, _verticalDirection) * movementAcceleration);

        var currentMaxMovementSpeed = movementState switch {
            PlayerMovementState.Sneaking => baseMovementSpeed * sneakingSpeedMultiplier,
            PlayerMovementState.Walking => baseMovementSpeed,
            PlayerMovementState.Running => baseMovementSpeed * runningSpeedMultiplier,
            _ => throw new ArgumentOutOfRangeException()
        };

        // Max Speed on X Axis
        if (Mathf.Abs(velocity.x) > currentMaxMovementSpeed) {
            _rb2d.velocity = new Vector2(Mathf.Sign(velocity.x) * currentMaxMovementSpeed, velocity.y).normalized;
        }

        // Max Speed on Y Axis
        if (Mathf.Abs(velocity.y) > currentMaxMovementSpeed) {
            _rb2d.velocity = new Vector2(velocity.x, Mathf.Sign(velocity.y) * currentMaxMovementSpeed).normalized;
        }
    }

    // Public functions
    public PlayerMovementState GetPlayerMovementState() {
        return movementState;
    }

    public float GetHorizontalDirection() {
        return _horizontalDirection;
    }

    public float GetVerticalDirection() {
        return _verticalDirection;
    }
    
}
