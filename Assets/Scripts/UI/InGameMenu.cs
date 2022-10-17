using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {
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
