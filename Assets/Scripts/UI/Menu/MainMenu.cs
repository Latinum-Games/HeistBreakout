using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClick_Play()
    {
        MenuManager.OpenMenu(Menu.LOBBY_MENU, gameObject);
    }

/*
    public void OnClick_Options()
    {
        MenuManager.OpenMenu(Menu.OPTIONS_MENU, gameObject);
    }
*/

    public void OnClick_Museum()
    {
        MenuManager.OpenMenu(Menu.MUSEUM_MENU, gameObject);
    }

    public void OnClick_Shop()
    {
        MenuManager.OpenMenu(Menu.SHOP_MENU, gameObject);
    }
    
    public void OnClick_Exit()
    {
        //MenuManager.OpenMenu(Menu.EXIT_MENU, gameObject);
        Application.Quit();
    }
    
}
