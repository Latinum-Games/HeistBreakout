using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {

    public void OpenMenu() {
        this.gameObject.SetActive(true);
    }
    
    public void CloseMenu() {
        this.gameObject.SetActive(false);
    }
}
