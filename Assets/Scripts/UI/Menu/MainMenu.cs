using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    //Lobby menu load
    public void OnClick_Play() {
        MenuManager.OpenMenu(Menu.LOBBY_MENU, gameObject);
    }

    //Museum menu load
    public void OnClick_Museum() {
        MenuManager.OpenMenu(Menu.MUSEUM_MENU, gameObject);
    }

    //Shop menu load
    public void OnClick_Shop() {
        MenuManager.OpenMenu(Menu.SHOP_MENU, gameObject);
    }
    
    //Exit app 
    public void OnClick_Exit() {
        //MenuManager.OpenMenu(Menu.EXIT_MENU, gameObject);
        Application.Quit();
    }
    
}
