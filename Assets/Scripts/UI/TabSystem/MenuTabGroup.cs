using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTabGroup : MonoBehaviour {

    public List<MenuTabButton> tabButtons;
    
    // TODO: CHECK HOW THE BUTTONS ARE GOING TO BE UPDATED IN TERMS OF UI
    public Color idleColor;
    public Color hoverColor;
    public Color selectedColor;
    
    private MenuTabButton selectedTab;
    public List<GameObject> objectsToSwap; // TODO: OPTIMIZE THE WAY THAT THE ITEMS ARE ORGANIZED

    private void Start() {
        foreach (var currentObject in objectsToSwap) {
            currentObject.SetActive(false);
        }
    }

    public void Subscribe(MenuTabButton button) {
        if (tabButtons == null) {
            tabButtons = new List<MenuTabButton>();
        }
        
        tabButtons.Add(button);
    }

    public void OnTabEnter(MenuTabButton button) {
        ResetTabs();

        if (selectedTab == null || button != selectedTab) {
            button.background.color = hoverColor;
        }
    }

    public void OnTabExit(MenuTabButton button) {
        ResetTabs();
    }

    public void OnTabSelected(MenuTabButton button) {
        selectedTab = button;
        ResetTabs();
        button.background.color = selectedColor;

        var index = button.transform.GetSiblingIndex();
        for (var i = 0; i < objectsToSwap.Count; i++) {
            objectsToSwap[i].SetActive(i == index);
        }
    }

    private void ResetTabs() {
        foreach (var button in tabButtons) {
            if (selectedTab != null && button == selectedTab) {
                continue;
            }
            
            button.background.color = idleColor; 
        }
    }
}
