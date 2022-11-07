using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementV2 : MonoBehaviour {
    
    //Initializes 3 stats of player movement
    public enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    //Initializes components of view, rigidbody and animator
    [Header("Components")]
    [SerializeField] private FieldOfView fieldOfView;
    private Rigidbody2D rb2d;
    public Animator animator;

    //Initializes states for movement and looking
    [Header("States")]
    [SerializeField] public PlayerMovementState movementState;
    [SerializeField] public PlayerLookState lookState;
    [SerializeField] public PlayerLookState lastLookState;

    //Initializes for movement variables including directions and angles
    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    [SerializeField] private float sneakingSpeedDivider = 2f;
    [SerializeField] private float runningSpeedMultiplier = 2.5f;
    private Vector2 inputVector;
    public float horizontalDirection;
    public float verticalDirection;
    private Vector2 dirVec;
    private float angleVec;
    
    //Initializes player look states
    public enum PlayerLookState{
        Up,
        Down,
        Left,
        Right,
        Idle
    }

    private void Start() {
        
        //Initialization of states and rigidbody
        inputVector = Vector2.zero;
        movementState = PlayerMovementState.Walking;
        lookState = PlayerLookState.Up;
        animator.SetInteger("Movement", 2);
        
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
    }

    private void Update() {
        //Assignment of directions and speed value update
        horizontalDirection = inputVector.x;
        verticalDirection = inputVector.y;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection) + Mathf.Abs(verticalDirection));
        
    }

    private void FixedUpdate() {
        //Character movement with fov position update
        MoveCharacter();
        fieldOfView.SetOrigin(transform.position);
        rb2d.drag = linearDrag;

    }
    
    //Movement of character
    private void MoveCharacter() {
        var velocity = rb2d.velocity;
        rb2d.AddForce(new Vector2(horizontalDirection, verticalDirection) * movementAcceleration);
        
        //Change in look states
        if(horizontalDirection==1 && verticalDirection==0)
        {
        lookState= PlayerLookState.Right;
        }
        else if(horizontalDirection==0 && verticalDirection==-1)
        {
        lookState= PlayerLookState.Down;
        }
        else if(horizontalDirection==-1 && verticalDirection==0)
        {
        lookState= PlayerLookState.Left;
        }
        else if(horizontalDirection==0 && verticalDirection==1)
        {
        lookState= PlayerLookState.Up;
        }
        else if(horizontalDirection==0 && verticalDirection==0)
        {
            lastLookState=lookState;
        }

        // TODO ADD THE MOVEMENT ACCELERATION TO THE MOVEMENT SPEED CALCULATIONS FOR THE STATES
        
        //Max speed controllers based in mmovement states
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
    
    //Setter for input based in 2d vector
    public void SetInput(Vector2 vector2) {
        inputVector = vector2;
    }

    //Setter for movement state based in switching
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
    
    //Getter for player movement state
    public PlayerMovementState GetPlayerMovementState() {
        return movementState;
    }

    //Getter for horizontal direction
    public float GetHorizontalDirection() {
        return horizontalDirection;
    }

    //Getter for vertical direction
    public float GetVerticalDirection() {
        return verticalDirection;
    }
    
    //Getter for player look state
    public PlayerLookState GetPlayerLookState()
    {
        return lookState;
    }
}
