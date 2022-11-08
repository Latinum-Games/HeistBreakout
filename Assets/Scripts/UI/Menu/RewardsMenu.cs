using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardsMenu : MonoBehaviour
{
    //
    public void OnClick_Play() {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
        MenuManager.Init();
    }

    public void OnClick_Exit() {
        SceneManager.LoadScene("UI Heist Breakout");
        MenuManager.Init();
    }

}
