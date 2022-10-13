using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuManager
{

    public static bool IsInitialised { get; private set;}
    public static GameObject mainMenu, optionsMenu, exitMenu, pauseMenu, shopMenu, rewardMenu, gameOverMenu, lobbyMenu, museumMenu  ;

    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("Title screen").gameObject;
        //exitMenu = canvas.transform.Find("ExitMenu").gameObject;
        
        //optionsMenu = canvas.transform.Find("OptionsMenu").gameObject;
        
        //pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        shopMenu = canvas.transform.Find("Shop").gameObject;
        //rewardMenu = canvas.transform.Find("RewardMenu").gameObject;
        //gameOverMenu = canvas.transform.Find("GameOverMenu").gameObject;
        lobbyMenu = canvas.transform.Find("Lobby").gameObject;
        museumMenu = canvas.transform.Find("My Museum").gameObject;

        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (!IsInitialised)
            Init();

        switch (menu)
        {
                
            case Menu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            //case Menu.OPTIONS_MENU:
                //optionsMenu.SetActive(true);
                //break;
                /*
            case Menu.EXIT_MENU:
                exitMenu.SetActive(true);
                break;
            
            case Menu.PAUSE_MENU:
                pauseMenu.SetActive(true);
                break;
                */
            case Menu.SHOP_MENU:
                shopMenu.SetActive(true);
                break;
                /*
            case Menu.REWARD_MENU:
                rewardMenu.SetActive(true);
                break;
            case Menu.GAME_OVER_MENU:
                gameOverMenu.SetActive(true);
                break;
                */
            case Menu.LOBBY_MENU:
                lobbyMenu.SetActive(true);
                break;
            case Menu.MUSEUM_MENU:
                museumMenu.SetActive(true);
                break;
                

        }

        callingMenu.SetActive(false);
    }

}