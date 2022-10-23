using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST_INPUT_CONTROLLER : MonoBehaviour {
    // Foreign Components
    [SerializeField] private InGamePauseMenuManager pauseMenuManager;
    
    // Auto-generated Actions
    private PlayerInputActions playerInputActions;
    
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        EnablePlayerOverworld();
    }

    private void EnablePlayerOverworld() {
        playerInputActions.PlayerOverworld.Enable();
        playerInputActions.PlayerOverworld.OpenMenu.performed += OpenMenu;
    }

    private void DisablePlayerOverworld() {
        playerInputActions.PlayerOverworld.Disable();
    }

    private void EnableMenu() {
        playerInputActions.MenuActions.Enable();
        playerInputActions.MenuActions.ExitMenu.performed += CloseMenu;
    }

    private void DisableMenu() {
        playerInputActions.MenuActions.Disable();
    }
    
    public void OpenMenu(InputAction.CallbackContext callback) {
        if (callback.performed) {
            Debug.Log("open menu");
            DisablePlayerOverworld();
            EnableMenu();
            
            pauseMenuManager.OpenMenu();
        }        
    }
    
    public void CloseMenu(InputAction.CallbackContext callback) {
        if (callback.performed) {
            Debug.Log("close menu");
            DisableMenu();
            EnablePlayerOverworld();
            
            pauseMenuManager.CloseMenu();
        } 
    }
}
