using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementV2 : MonoBehaviour {
    
    public enum PlayerMovementState {
        Sneaking,
        Walking,
        Running
    }

    [Header("Components")]
    [SerializeField] private FieldOfView fieldOfView;
    private Rigidbody2D rb2d;
    public Animator animator;

    [Header("States")]
    [SerializeField] public PlayerMovementState movementState;
    [SerializeField] public PlayerLookState lookState;


    [Header("Movement Variables")]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float baseMovementSpeed;
    [SerializeField] private float linearDrag;
    [SerializeField] private float sneakingSpeedDivider = 2f;
    [SerializeField] private float runningSpeedMultiplier = 2.5f;
    private Vector2 inputVector;
    private float horizontalDirection;
    private float verticalDirection;
    private Vector2 dirVec;
    private float angleVec;
    private bool isMoving =false;

    public enum PlayerLookState{
        Up,
        Down,
        Left,
        Right
    }
    [Header("Hitboxes")]
    public GameObject upHit;
    public GameObject downHit;
    public GameObject leftHit;
    public GameObject rightHit;
    
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    
    
    

private void Awake()
{
    
}
    private void Start() {
        inputVector = Vector2.zero;
        movementState = PlayerMovementState.Walking;
        lookState = PlayerLookState.Up;
        animator.SetInteger("Movement", 2);
        
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = linearDrag;
    }

    private void Update() {
        horizontalDirection = inputVector.x;
        verticalDirection = inputVector.y;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection) + Mathf.Abs(verticalDirection));
        if(checkMove())
        {Hitboxer();}
        
        
        
        
    }

    private void FixedUpdate() {
        MoveCharacter();
        fieldOfView.SetOrigin(transform.position);
        rb2d.drag = linearDrag;

    }

    
    
    private bool checkMove()
    {
        if(verticalDirection==0 && horizontalDirection==0 )
        isMoving=false;
        else
        isMoving=true;

        return isMoving;
    }
    private void Hitboxer()
    {if(lookState== PlayerLookState.Up)
        {
            upHit.SetActive(true);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            up=true;
            down=false;
            left=false;
            right=false;
            
        }
        else if(lookState== PlayerLookState.Down)
        {
            upHit.SetActive(false);
            downHit.SetActive(true);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            up=false;
            down=true;
            left=false;
            right=false;
        }
        else if(lookState== PlayerLookState.Right)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(true);
            up=false;
            down=false;
            left=false;
            right=true;
        }
        else if(lookState== PlayerLookState.Left)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(true);
            rightHit.SetActive(false);
            up=false;
            down=false;
            left=true;
            right=false;
        }
        else{
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
        }}

    private void MoveCharacter() {
        var velocity = rb2d.velocity;
        rb2d.AddForce(new Vector2(horizontalDirection, verticalDirection) * movementAcceleration);
        dirVec =new Vector2(horizontalDirection, verticalDirection);
        angleVec =Angle(dirVec);
        

        //SET ANGLE STATE DEPENDING IN ANGLE DIRECTION IN CLOSED SECTION
        if(angleVec==90)
        {
        lookState= PlayerLookState.Right;
        }
        else if(angleVec==180)
        {
        lookState= PlayerLookState.Down;
        }
        else if(angleVec==270)
        {
        lookState= PlayerLookState.Left;
        }
        else if(angleVec==0)
        {
        lookState= PlayerLookState.Up;
        }


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
    public static float Angle(Vector2 vector2)
{
    if (vector2.x < 0)
    {
         return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * Mathf.Sign(vector2.x));;
    }
    else
    {
         return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
    }
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
