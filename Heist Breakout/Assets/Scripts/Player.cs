

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float inputSmoothDamp  = .3f;
    [SerializeField]
    private float smoothInputSpeed = .2f;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;

    private InputAction moveAction;
    private InputAction velocityAction;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    public float vecX;
    public float vecY;
    public float mag;



    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        velocityAction = playerInput.actions["Velocity"];
    }

    void Start()
    {
        
    }


    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        Vector3 move = new Vector3(currentInputVector.x, currentInputVector.y, 0);
        vecX = move.x;
        vecY = move.y;
        mag = move.magnitude;
        mag=Mathf.Clamp(mag, 0.1f, 1.0f);
        controller.Move(move * Time.deltaTime * playerSpeed);

        Debug.Log(move);
        //Debug.Log(mag);


    }
}
