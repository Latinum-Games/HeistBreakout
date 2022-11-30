using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUIMulti : MonoBehaviourPunCallbacks {
    
    // Buttons
    [SerializeField] private GameObject exitButton;
    
    private void Start() {
        //Gets button and adds listener to exit to main menu
        exitButton.GetComponent<Button>().onClick.AddListener(ExitToMainMenu);
    }

    //Loads main menu 
    private void ExitToMainMenu() {
        StartCoroutine("DisconnectAndLoad");
        Debug.Log("picado");
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
