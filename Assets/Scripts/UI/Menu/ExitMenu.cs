using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    public void OnClick_Yes()
    {
        SceneManager.LoadScene("UI Heist Breakout");
        MenuManager.Init();
    }

    public void OnClick_No()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }

}
