using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    public void OnClick_Yes()
    {
        StartCoroutine("Load");
        //TODO: Revisar carga 
    }

    public void OnClick_No()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }

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
