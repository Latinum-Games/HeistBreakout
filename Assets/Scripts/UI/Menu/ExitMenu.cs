using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    //Load scene when exit
    public void OnClick_Yes()
    {
        StartCoroutine("Load");
    }

    //Returns to pause menu 
    public void OnClick_No()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }

    //Corroutine to load main menu (UI Heist Breakout)
    private IEnumerator Load()
    {
        MenuManager.IsInitialised = false;
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync("UI Heist Breakout", LoadSceneMode.Single);
        while (!sceneLoading.isDone)
        {
            yield return null;
        }
        
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
        
    }

}
