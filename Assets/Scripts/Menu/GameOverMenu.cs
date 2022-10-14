using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void OnClick_Yes()
    {
        SceneManager.LoadScene("MVP 2.0");
        MenuManager.Init();
    }

    public void OnClick_No()
    {
        SceneManager.LoadScene("UI Heist Breakout");
        MenuManager.Init();
    }
}
