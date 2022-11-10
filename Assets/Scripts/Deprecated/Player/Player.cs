using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    // FIELD OF VIEW
    [SerializeField] private FieldOfView fieldOfView;

    [SerializeField] private float playerSpeed = 2.0f;
    //private float inputSmoothDamp  = .3f;
    [SerializeField] private float smoothInputSpeed = .2f;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;

    private InputAction moveAction;
    private InputAction velocityAction;
    private InputAction hitAction;

    private Vector2 currentInputVector;
    private Vector2 smoothInputVelocity;
    private float vecX;
    private float vecY;
    public float mag;

    [SerializeField] public float maxSoundArea = 2;

    public enum Velocity{ Slow,Normal,Fast}
    public Velocity velocity;
    private int velState=1;
    [SerializeField] private bool isChangingVel=true;
    [SerializeField] private bool isHitting=false;

    public GameObject HitBox;

    void Awake() {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        velocityAction = playerInput.actions["Velocity"];
        hitAction = playerInput.actions["Hit"];
    }

    void Start() {
        
    }

    void Update() {
        if(velState==0) {
            velocity=Velocity.Slow;
            maxSoundArea=2f;
            playerSpeed=4f;

        } if(velState==1) {
            velocity=Velocity.Normal;
            maxSoundArea=5f;
            playerSpeed=5f;

        } if(velState==2) {
            velocity=Velocity.Fast;
            maxSoundArea=7f;
            playerSpeed=7f;

        } if(velocityAction.IsPressed()&&isChangingVel) {
            StartCoroutine(changeVel());

        } if(hitAction.IsPressed()&&!isHitting) {
            StartCoroutine(actHitBox());

        }
        //Debug.Log(velocityAction.IsPressed());

        Vector2 input = moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVelocity, smoothInputSpeed);
        Vector3 move = new Vector3(currentInputVector.x, currentInputVector.y, 0);
        // vecX = move.x;
        // vecY = move.y;
        mag = move.magnitude;
        
        mag = Mathf.Clamp(mag, 0.1f, 1.0f);
        controller.Move(move * Time.deltaTime * playerSpeed);


        fieldOfView.SetOrigin(transform.position);

        //Debug.Log(transform.position);
        //Debug.Log(mag);
    }

    IEnumerator changeVel() {
        isChangingVel=false;

        if(velState == 2) {
            velState = 0;
        } else {
            velState++;
        }

        yield return new WaitForSeconds(1);
        isChangingVel=true;
    }
    IEnumerator actHitBox() {
        isHitting = true;
        HitBox.SetActive(true);
        yield return new WaitForSeconds(1);
        HitBox.SetActive(false);
        isHitting=false;
    }
}
