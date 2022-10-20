using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGameMenu : MonoBehaviour {

    // Public Event System for Navigation
    public EventSystem eventSystem;

    private void Awake() {
        eventSystem.firstSelectedGameObject = GameObject.Find("TabArea/InventoryTab");
    }

    private void Start() {
        transform.localScale = Vector2.zero;
    }

    public void OpenMenu() {
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
    }
    
    public void CloseMenu() {
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), 0.6f).setEaseOutBounce();
    }
}
