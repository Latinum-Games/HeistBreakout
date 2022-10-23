using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePauseMenuManager : MonoBehaviour {
    public enum PauseMenuScreens {
        Inventory,
        Options
    }

    // Event Manager
    // TODO: ADD EVENT MANAGER
    // TODO: ADD LOGIC FOR THE STATIC NAVIGATION ITEMS
    // TODO: ADD LOGIC FOR THE SELECTED ELEMENT ON EACH PART OF THE MENU
    
    

    public void OpenMenu() {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }
    
    public void CloseMenu() {
        LeanTween.scale(gameObject, new Vector3(0.5f, 0.5f, 0.5f), 0.6f).setEaseOutBounce();
    }
    
    
}
