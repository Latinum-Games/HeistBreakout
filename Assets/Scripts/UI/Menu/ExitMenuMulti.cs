using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenuMulti : MonoBehaviourPunCallbacks
{
    [PunRPC]
    //Load scene when exit
    public void OnClick_Yes()
    {
        StartCoroutine("DisconnectAndLoad");
    }

    //Returns to pause menu 
    public void OnClick_No()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }

    [PunRPC]
    //Corroutine to load main menu (UI Heist Breakout)
    private IEnumerator DisconnectAndLoad()
    {
        MenuManager.IsInitialised = false;
        
        _NetworkManager.instance.DisconnectPlayer();
        Destroy(_NetworkManager.instance.gameObject);

        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync("UI Heist Breakout", LoadSceneMode.Single);
        while (!sceneLoading.isDone)
        {
            yield return null;
        }
        
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
        
    }

}
