using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGamePauseMenuManager : MonoBehaviour {
    // Tab GameObjects
    [Header("Menu Tab Options")]
    [SerializeField] private int currentScreenIndex;
    [SerializeField] private List<GameObject> menuButtons;
    [SerializeField] private List<GameObject> menuScreens;

    private void Awake() {
        UpdateScreens(currentScreenIndex);
        
        // Add button listeners
        for (var i = 0; i < menuButtons.Count; i++) {
            var target = i;
            menuButtons[i].GetComponent<Button>().onClick.AddListener(delegate { UpdateScreens(target); });
        }
    }

    public void OpenMenu() {
        // Animation
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEaseOutElastic();
        
        // Clear Selected Object in Event System
        EventSystem.current.SetSelectedGameObject(null);
        
        // Set New Selected Object
        EventSystem.current.SetSelectedGameObject(menuButtons[0]);
        UpdateScreens(0);
    }
    
    public void CloseMenu() {
        // Animation
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), 0.6f).setEaseOutBounce();
        
        // Clear Selected Object in Event System
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateScreens(int target) {
        currentScreenIndex = target;
        
        for (var i = 0; i < menuScreens.Count; i++) {
            //Animation
            LeanTween.cancel(menuScreens[i].gameObject);
            LeanTween.scale(menuScreens[i].gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEasePunch();
            
            // Set Active Screen
            menuScreens[i].gameObject.SetActive(i == target);
        }
    }
}
