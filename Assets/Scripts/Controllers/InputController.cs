using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    // Components 
    private MovementV2 movementV2;
    private Interactor interactor;

    // Auto-generated Actions
    private PlayerInputActions playerInputActions;

    // Start is called before the first frame update
    void Awake() {
        movementV2 = GetComponent<MovementV2>();
        interactor = GetComponent<Interactor>();
        playerInputActions = new PlayerInputActions();
        SubscribePlayerOverworldMap();
    }

    // Update is called once per frame
    void Update() {
        MoveCharacter();
    }

    // Subscribers
    // NOTE: Subscribe only the inputs that does not require an update each frame
    private void SubscribePlayerOverworldMap() {
        playerInputActions.PlayerOverworld.Enable();
        playerInputActions.PlayerOverworld.Sneak.performed += Sneak;
        playerInputActions.PlayerOverworld.Sneak.canceled += Sneak;
        playerInputActions.PlayerOverworld.Run.performed += Run;
        playerInputActions.PlayerOverworld.Run.canceled += Run;
        playerInputActions.PlayerOverworld.Interact.performed += Interact;
    }

    // Player Overworld Actions
    private void MoveCharacter() {
        var inputVector = playerInputActions.PlayerOverworld.Move.ReadValue<Vector2>();
        movementV2.SetInput(inputVector);
    }

    private void Sneak(InputAction.CallbackContext context) {
        movementV2.SetMovementState(context.performed ? 1 : 2);
    }

    private void Run(InputAction.CallbackContext context) {
        movementV2.SetMovementState(context.performed ? 3 : 2);
    }

    private void Interact(InputAction.CallbackContext context) {
        if (context.performed) {
            interactor.InteractAction(true);
        }
    }
}
