using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager {
    //Initializer for menus at global level
    [SerializeField] public static bool IsInitialised { get; set;}
    //Initializer of menus
    public static GameObject mainMenu, optionsMenu, exitMenu, pauseMenu, shopMenu, rewardMenu, gameOverMenu, lobbyMenu, museumMenu;
    //Current scene
    private static string currentScene;

    public static void Init() {
        //Canvas initializer with respective scene
        GameObject canvas = GameObject.Find("Canvas");
        currentScene = SceneManager.GetActiveScene().name;

        //Menu initializer depending on main menu or in game menu
        if (currentScene == "UI Heist Breakout") {
        mainMenu = canvas.transform.Find("Title screen").gameObject;
        shopMenu = canvas.transform.Find("Shop").gameObject;
        lobbyMenu = canvas.transform.Find("Lobby").gameObject;
        museumMenu = canvas.transform.Find("My Museum").gameObject;

        } else if(currentScene == "Interfaces 2") {
            exitMenu = canvas.transform.Find("Exit menu").gameObject;
            optionsMenu = canvas.transform.Find("Options menu").gameObject;
            pauseMenu = canvas.transform.Find("Pause menu").gameObject;
            rewardMenu = canvas.transform.Find("Rewards menu").gameObject;
            gameOverMenu = canvas.transform.Find("Game Over menu").gameObject;
        }
        //Blocked menu changes
        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu) {
        //Checker for menu initialization
        if (!IsInitialised)
        	Init();
        
        //All menu cases
        switch (menu) {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            case Menu.OPTIONS_MENU:
                optionsMenu.SetActive(true);
                break;
            case Menu.EXIT_MENU:
                exitMenu.SetActive(true);
                break;
            case Menu.PAUSE_MENU:
                pauseMenu.SetActive(true);
                break;
            case Menu.SHOP_MENU:
                shopMenu.SetActive(true);
                break;
            case Menu.REWARD_MENU:
                rewardMenu.SetActive(true);
                break;
            case Menu.GAME_OVER_MENU:
                gameOverMenu.SetActive(true);
                break;
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
