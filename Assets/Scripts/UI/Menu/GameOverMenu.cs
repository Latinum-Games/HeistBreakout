using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    // Loading of 
    public void OnClick_Yes() {
        SceneManager.LoadScene("UI Heist Breakout");
        MenuManager.Init();
    }

    public void OnClick_No() {
        string activeScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeScene);
        MenuManager.Init();
    }
}
