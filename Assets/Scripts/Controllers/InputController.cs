using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    private enum InputMapType {
        Player,
        Menu
    }
    
    // Foreign Components
    [SerializeField] private InGamePauseMenuManager inGameMenu;
    
    // Components 
    private MovementV2 movementV2;
    private Interactor interactor;
    private hitController hitController;

    // Auto-generated Actions
    private PlayerInputActions playerInputActions;
    
    // Variables
    private InputMapType currentInputMap;

    // Start is called before the first frame update
    private void Awake() {
        movementV2 = GetComponent<MovementV2>();
        hitController= GetComponent<hitController>();
        interactor = GetComponent<Interactor>();
        playerInputActions = new PlayerInputActions();
        SubscribePlayerOverworldMap();
    }

    // Update is called once per frame
    private void Update() {
        if (currentInputMap == InputMapType.Player) {
            MoveCharacter();
        }
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
        playerInputActions.PlayerOverworld.Hit.performed += SetHit;
        playerInputActions.PlayerOverworld.OpenMenu.performed += OpenMenu;
        currentInputMap = InputMapType.Player;
    }
    
    private void DisablePlayerOverworldMap() {
        playerInputActions.PlayerOverworld.Disable();
    }

    private void SubscribeMenuMap() {
        playerInputActions.MenuActions.Enable();
        playerInputActions.MenuActions.ExitMenu.performed += CloseMenu;
        currentInputMap = InputMapType.Menu;
    }

    private void DisableMenuMap() {
        playerInputActions.MenuActions.Disable();
    }

    // Player Overworld Actions
    private void MoveCharacter() {
        var inputVector = playerInputActions.PlayerOverworld.Move.ReadValue<Vector2>();
        movementV2.SetInput(inputVector);
    }

    private void SetHit(InputAction.CallbackContext context) {
        
        hitController.SetHit(context.performed);
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
    
    private void OpenMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            DisablePlayerOverworldMap();
            SubscribeMenuMap();

            // Hide Character and HUD TODO: HIDE HUD 
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            
            // Open Menu
            inGameMenu.OpenMenu();
        }
    }
    
    // Menu Actions
    private void CloseMenu(InputAction.CallbackContext context) {
        if (context.performed) {
            DisableMenuMap();
            SubscribePlayerOverworldMap();
            
            // SHOW Character and HUD TODO: SHOW HUD 
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            
            // Close Menu
            inGameMenu.CloseMenu();
        }
    }
}
