using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGamesMenu : MonoBehaviour
{
    //Loading of Minigames menu
    public void OnClick_Minigames() {
        SceneManager.LoadScene("SimonSaysMinigame");
    }
    
    //Loading of Main Menu
    public void OnClick_ReturnMainMenu() {
        StartCoroutine("Load");
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
