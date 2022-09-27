using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementV2 : MonoBehaviour {

    private enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    [Header("Components")]
    private Rigidbody2D rb2d;
    [SerializeField] private PlayerMovementState movementState = PlayerMovementState.Walking;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    private float horizontalDirection;
    private float verticalDirection;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
    }

    private void Update() {
        horizontalDirection = GetInput().x;
        verticalDirection = GetInput().y;
    }

    private void FixedUpdate() {
        SwitchWalkingState();
        MoveCharacter();
        // rb2d.drag = linearDrag;

    }

    // Get input controllers
    private static Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // TODO: UPDATE TO THE NEW INPUT SYSTEM
    private void SwitchWalkingState() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            movementState = PlayerMovementState.Running;
        } else if (Input.GetKey(KeyCode.Space)) {
            movementState = PlayerMovementState.Sneaking;
        } else {
            movementState = PlayerMovementState.Walking;
        }
    }

    private void MoveCharacter() {
        rb2d.AddForce(new Vector2(horizontalDirection, verticalDirection) * movementAcceleration);

        float currentMaxMovementSpeed = 0F;
        switch (movementState) {
            case PlayerMovementState.Sneaking:
                currentMaxMovementSpeed = baseMovementSpeed / 2.5f;
                break;
            case PlayerMovementState.Walking:
                currentMaxMovementSpeed = baseMovementSpeed;
                break;
            case PlayerMovementState.Running:
                currentMaxMovementSpeed = baseMovementSpeed * 2;
                break;
        }

        // Max Speed on X Axis
        if (Mathf.Abs(rb2d.velocity.x) > currentMaxMovementSpeed) {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * currentMaxMovementSpeed, rb2d.velocity.y);
        }

        // Max Speed on Y Axis
        if (Mathf.Abs(rb2d.velocity.y) > currentMaxMovementSpeed) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * currentMaxMovementSpeed);
        }
    }

}
