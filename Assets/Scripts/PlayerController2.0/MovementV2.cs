using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementState {
    Sneaking,
    Walking,
    Running
}

public class MovementV2 : MonoBehaviour {

    [Header("Components")]
    private Rigidbody2D rb2d;
    [SerializeField] private PlayerMovementState movementState = PlayerMovementState.Walking;

    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float maxMovementSpeed;
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
        MoveCharacter();
        // rb2d.drag = linearDrag;

    }

    // Get input controllers
    private static Vector2 GetInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter() {
        rb2d.AddForce(new Vector2(horizontalDirection, verticalDirection) * movementAcceleration);

        float currentMaxMovementSpeed = 0F;
        switch (movementState) {
            case PlayerMovementState.Sneaking:
                currentMaxMovementSpeed = maxMovementSpeed / 2;
                break;
            case PlayerMovementState.Walking:
                currentMaxMovementSpeed = maxMovementSpeed;
                break;
            case PlayerMovementState.Running:
                currentMaxMovementSpeed = maxMovementSpeed * 2;
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
