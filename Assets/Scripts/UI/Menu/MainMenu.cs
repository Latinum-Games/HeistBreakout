using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    // Foreign Components -> IMPROVE LOGIC
    [Header("FOREING COMPONENTS -> REMOVE AFTER")] 
    [SerializeField] private MiniGamesMenu miniGamesMenu;
    
    // Splash Art Components

    [Header("Heist Ops Tile")]
    [SerializeField] private GameObject heistOpsTileParent;
    [SerializeField] private GameObject heistOpsUnselectedTitle;
    [SerializeField] private GameObject heistOpsSelectedContent;
    [SerializeField] private GameObject heistOpsPositionIn;
    [SerializeField] private GameObject heistOpsPositionOut;
    
    [Header("Multiplayer Tile")]
    [SerializeField] private GameObject multiplayerTileParent;
    [SerializeField] private GameObject multiplayerUnselectedTitle;
    [SerializeField] private GameObject multiplayerSelectedContent;
    [SerializeField] private GameObject multiplayerPositionIn;
    [SerializeField] private GameObject multiplayerPositionOut;

    [Header("Extra Options Tile")] 
    [SerializeField] private GameObject extraTilesParent;
    [SerializeField] private GameObject extraTilesPositionIn;
    [SerializeField] private GameObject extraTilesPositionOut;

    [Header("Museum Tile")]
    [SerializeField] private GameObject museumTileParent;
    [SerializeField] private GameObject museumUnselectedTitle;
    [SerializeField] private GameObject museumSelectedContent;

    [Header("Mini Games Tile")] 
    [SerializeField] private GameObject miniGameTileParent;
    [SerializeField] private GameObject miniGameUnselectedTitle;
    [SerializeField] private GameObject miniGameSelectedContent;
    
    [Header("Shop Tile")]
    [SerializeField] private GameObject shopTileParent;
    [SerializeField] private GameObject shopUnselectedTitle;
    [SerializeField] private GameObject shopSelectedContent;
    
    [Header("Quit Game Tile")]
    [SerializeField] private GameObject quitTileParent;
    [SerializeField] private GameObject quitUnselectedTitle;
    [SerializeField] private GameObject quitSelectedContent;

    private bool isSceneFullyLoaded;

    private void Start() {
        var basePath = "Canvas/TitleScreen/";
        
        // Heist Ops Tile
        heistOpsTileParent = GameObject.Find(basePath + "MenuOptions/HeistOpsTile");
        heistOpsUnselectedTitle = heistOpsTileParent.transform.Find("Tile/UnselectedTitle").gameObject;
        heistOpsSelectedContent = heistOpsTileParent.transform.Find("Tile/SelectedContent").gameObject;
        heistOpsPositionIn = heistOpsTileParent.transform.Find("PositionIn").gameObject;
        heistOpsPositionOut = heistOpsTileParent.transform.Find("PositionOut").gameObject;

        // Multiplayer Tile
        multiplayerTileParent = GameObject.Find(basePath + "MenuOptions/RightGrid/MultiplayerTile");
        multiplayerUnselectedTitle = multiplayerTileParent.transform.Find("Tile/UnselectedTitle").gameObject;
        multiplayerSelectedContent = multiplayerTileParent.transform.Find("Tile/SelectedContent").gameObject;
        multiplayerPositionIn = multiplayerTileParent.transform.Find("PositionIn").gameObject;
        multiplayerPositionOut = multiplayerTileParent.transform.Find("PositionOut").gameObject;
        
        // Extra Tiles
        extraTilesParent = GameObject.Find(basePath + "MenuOptions/RightGrid/ExtraTiles");
        extraTilesPositionIn = extraTilesParent.transform.Find("PositionIn").gameObject;
        extraTilesPositionOut = extraTilesParent.transform.Find("PositionOut").gameObject;
        
        // Museum Tile
        museumTileParent = extraTilesParent.transform.Find("Tiles/MuseumTile").gameObject;
        museumUnselectedTitle = museumTileParent.transform.Find("UnselectedTitle").gameObject;
        museumSelectedContent = museumTileParent.transform.Find("SelectedContent").gameObject;
        
        // Mini Games Tile
        miniGameTileParent = extraTilesParent.transform.Find("Tiles/MinigameTile").gameObject;
        miniGameUnselectedTitle = miniGameTileParent.transform.Find("UnselectedTitle").gameObject;
        miniGameSelectedContent = miniGameTileParent.transform.Find("SelectedContent").gameObject;
        
        // Shop Tile
        shopTileParent = extraTilesParent.transform.Find("Tiles/ShopTile").gameObject;
        shopUnselectedTitle = shopTileParent.transform.Find("UnselectedTitle").gameObject;
        shopSelectedContent = shopTileParent.transform.Find("SelectedContent").gameObject;
        
        // Quit Tile
        quitTileParent = extraTilesParent.transform.Find("Tiles/ExitTile").gameObject;
        quitUnselectedTitle = quitTileParent.transform.Find("UnselectedTitle").gameObject;
        quitSelectedContent = quitTileParent.transform.Find("SelectedContent").gameObject;
        
        // Set Positions
        heistOpsTileParent.transform.Find("Tile").position = heistOpsPositionOut.transform.position;
        multiplayerTileParent.transform.Find("Tile").position = multiplayerPositionOut.transform.position;
        extraTilesParent.transform.Find("Tiles").position = extraTilesPositionOut.transform.position;
        
        // Set Listeners
        SetTileListeners();
        
        // Display Intro Menu Animation
        MainMenu_OpenAnimation();

        isSceneFullyLoaded = true;
    }

    private void OnEnable() {
        if (isSceneFullyLoaded) {
            MainMenu_OpenAnimation();
        }
    }

    private void SetTileListeners() {
        // HeistOps Tile
        var heistOpsButton = heistOpsTileParent.GetComponent<Button>();
        var heistOpsButtonEvents = heistOpsTileParent.GetComponent<ButtonEventHandlers>();
        heistOpsButton.onClick.AddListener(OnClick_Play);
        heistOpsButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(heistOpsUnselectedTitle, heistOpsSelectedContent); });
        heistOpsButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(heistOpsUnselectedTitle, heistOpsSelectedContent); });
        
        // Multiplayer Tile
        var multiplayerButton = multiplayerTileParent.GetComponent<Button>();
        var multiplayerButtonEvents = multiplayerTileParent.GetComponent<ButtonEventHandlers>();
        multiplayerButton.onClick.AddListener(OnClick_Multiplayer);
        multiplayerButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(multiplayerUnselectedTitle, multiplayerSelectedContent); });
        multiplayerButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(multiplayerUnselectedTitle, multiplayerSelectedContent); });
        
        // Museum Tiles
        var museumButton = museumTileParent.GetComponent<Button>();
        var museumButtonEvents = museumTileParent.GetComponent<ButtonEventHandlers>();
        museumButton.onClick.AddListener(OnClick_Museum);
        museumButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(museumUnselectedTitle, museumSelectedContent); });
        museumButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(museumUnselectedTitle, museumSelectedContent); });
        
        // Mini Game Tile
        var miniGameButton = miniGameTileParent.GetComponent<Button>();
        var miniGameButtonEvents = miniGameTileParent.GetComponent<ButtonEventHandlers>();
        miniGameButton.onClick.AddListener(OnClick_MiniGame);
        miniGameButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(miniGameUnselectedTitle, miniGameSelectedContent); });
        miniGameButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(miniGameUnselectedTitle, miniGameSelectedContent); });
        
        // Shop Tile
        var shopButton = shopTileParent.GetComponent<Button>();
        var shopButtonEvents = shopTileParent.GetComponent<ButtonEventHandlers>();
        shopButton.onClick.AddListener(OnClick_Shop);
        shopButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(shopUnselectedTitle, shopSelectedContent); });
        shopButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(shopUnselectedTitle, shopSelectedContent); });
        
        // Quit Tile
        var quitButton = quitTileParent.GetComponent<Button>();
        var quitButtonEvents = quitTileParent.GetComponent<ButtonEventHandlers>();
        quitButton.onClick.AddListener(OnClick_Exit);
        quitButtonEvents.onSelectAction.AddListener(delegate { OnSelectTween(quitUnselectedTitle, quitSelectedContent); });
        quitButtonEvents.onDeselectAction.AddListener(delegate { OnDeselectTween(quitUnselectedTitle, quitSelectedContent); });
    }

    //Lobby menu load
    public void OnClick_Play() {
        MainMenu_CloseAnimation(() => { MenuManager.OpenMenu(Menu.LOBBY_MENU, gameObject); });
    }

    public void OnClick_Multiplayer() {
        // TODO: MAKE TRANSITION
        MainMenu_CloseAnimation(() => { MenuManager.OpenMenu(Menu.LOBBY_MENU_MULTI, gameObject); });
    }
    
    //Museum menu load
    public void OnClick_Museum() {
        MainMenu_CloseAnimation(() => { MenuManager.OpenMenu(Menu.MUSEUM_MENU, gameObject); });
    }

    //Shop menu load
    public void OnClick_Shop() {
        MainMenu_CloseAnimation(() => { MenuManager.OpenMenu(Menu.SHOP_MENU, gameObject); });
    }

    public void OnClick_MiniGame() {
        MainMenu_CloseAnimation(() => { miniGamesMenu.OnClick_Minigames(); });
    }
    
    //Exit app 
    public void OnClick_Exit() {
        //MenuManager.OpenMenu(Menu.EXIT_MENU, gameObject);
        Application.Quit();
    }
    
    // GameObject Animations
    private void MainMenu_OpenAnimation() {
        var heistOpsTile = heistOpsTileParent.transform.Find("Tile").gameObject;
        var multiplayerTile = multiplayerTileParent.transform.Find("Tile").gameObject;
        var extraTiles = extraTilesParent.transform.Find("Tiles").gameObject;
        
        // HeistOps Tile
        LeanTween.delayedCall(heistOpsTile, 1f, () => {
            LeanTween.cancel(heistOpsTile);
            LeanTween.move(heistOpsTile, heistOpsPositionIn.transform.position, 0.3f).setEaseOutExpo();
        });

        // Multiplayer Tile
        LeanTween.delayedCall(multiplayerTile, 1.1f, () => {
            LeanTween.cancel(multiplayerTile);
            LeanTween.move(multiplayerTile, multiplayerPositionIn.transform.position, 0.3f).setEaseOutExpo();
        });

        LeanTween.delayedCall(extraTiles, 1.2f, () => {
            LeanTween.cancel(extraTiles);
            LeanTween.move(extraTiles, extraTilesPositionIn.transform.position, 0.3f).setEaseOutExpo();
        });
    }

    private void MainMenu_CloseAnimation(Action action = null) {
        var heistOpsTile = heistOpsTileParent.transform.Find("Tile").gameObject;
        var multiplayerTile = multiplayerTileParent.transform.Find("Tile").gameObject;
        var extraTiles = extraTilesParent.transform.Find("Tiles").gameObject;
        
        LeanTween.cancel(heistOpsTile);
        LeanTween.move(heistOpsTile, heistOpsPositionOut.transform.position, 0.3f).setEaseOutExpo();
        
        LeanTween.delayedCall(multiplayerTile, 0.1f, () => {
            LeanTween.cancel(multiplayerTile);
            LeanTween.move(multiplayerTile, multiplayerPositionOut.transform.position, 0.3f).setEaseOutExpo();
        });
        
        LeanTween.delayedCall(extraTiles, 0.2f, () => {
            LeanTween.cancel(extraTiles);
            LeanTween.move(extraTiles, extraTilesPositionOut.transform.position, 0.3f).setEaseOutExpo().setOnComplete(action);
        });
    }
    
    // Tween Animations
    private void OnSelectTween(GameObject unselectedObj, GameObject selectedObj) {
        // Unselected Components
        unselectedObj.SetActive(false);

        // Selected Components
        SelectedTweenAnimation(selectedObj);
    }

    private void OnDeselectTween(GameObject unselectedObj, GameObject selectedObj) {
        // Unselected Components
        SelectedTweenAnimation(unselectedObj);

        // Selected Components
        selectedObj.SetActive(false);
    }
        
    private void SelectedTweenAnimation(GameObject obj) {
        obj.SetActive(true);
        obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        obj.transform.localScale = Vector3.one;

        LeanTween.cancel(obj);
        LeanTween.rotateZ(obj, 10f, 0.5f).setEasePunch();
        LeanTween.scale(obj, new Vector3(1.3f, 1.3f), 0.5f).setEasePunch();
    }
}
