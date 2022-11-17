using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    // Grid Layout Component
    [SerializeField] private GameObject gridLayout;
    
    // Grid Layout Children
    [SerializeField] private GameObject heistOpsTile;
    [SerializeField] private GameObject museumTile;
    [SerializeField] private GameObject extrasTile;

    private void Start() {
        // Set UI components with fixed names and properties
        gridLayout = GameObject.Find("GridLayout");
        heistOpsTile = GameObject.Find("GridLayout/HeistOpsTile");
        museumTile = GameObject.Find("GridLayout/RightGrid/MuseumTile");
        extrasTile = GameObject.Find("GridLayout/RightGrid/MuseumTile");
        
        SetTileListeners();
        
        // Display Intro Menu Animation
        // StartCoroutine(UIDelay());
        MainMenu_OpenAnimation();
    }
    
    private void SetTileListeners() {
        var heistOpsButton = heistOpsTile.GetComponent<Button>();
        var heistOpsButtonEvents = heistOpsTile.GetComponent<ButtonEventHandlers>();
        
        // HeistOps Tile
        heistOpsButton.onClick.AddListener(OnClick_Play);
        heistOpsButtonEvents.onSelectAction.AddListener(HeistOps_OnSelectTween);
        heistOpsButtonEvents.onDeselectAction.AddListener(HeistOps_OnDeselectTween);
    }

    //Lobby menu load
    public void OnClick_Play() {
        // MenuManager.OpenMenu(Menu.LOBBY_MENU, gameObject);
        
        // TODO: UPDATE TO DELEGATE
        MainMenu_CloseAnimation();
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
    
    // Tweenie Animations

    private void MainMenu_OpenAnimation() {
        // Menu components
        var tile = GameObject.Find("GridLayout/HeistOpsTile/Tile");
        
        // UI Positions
        var heistOpsTileInPosition = GameObject.Find("GridLayout/HeistOpsTile/PositionIn");
        
        // Tween Animation
        LeanTween.delayedCall(tile, 1f, () => {
            LeanTween.cancel(tile);
            LeanTween.move(tile, heistOpsTileInPosition.transform.position, 0.3f).setEaseOutExpo();
        });
    }

    private void MainMenu_CloseAnimation() {
        // Menu components
        var tile = GameObject.Find("GridLayout/HeistOpsTile/Tile");
        
        // UI Positions
        var heistOpsTileOutPosition = GameObject.Find("GridLayout/HeistOpsTile/PositionOut");
        
        // Tween Animation
        LeanTween.cancel(tile);
        LeanTween.move(tile, heistOpsTileOutPosition.transform.position, 0.3f)
            .setEaseOutExpo()
            .setOnComplete(() => {
                MenuManager.OpenMenu(Menu.LOBBY_MENU, gameObject);
            });
    }
    
    private void HeistOps_OnSelectTween() {
        // Unselected Components
        var unselectedTileText = GameObject.Find("GridLayout/HeistOpsTile/Tile/TileTitle");
        unselectedTileText.SetActive(false);


        // Selected Components
        var selectedTileText = GameObject.Find("GridLayout/HeistOpsTile/Tile/SelectedContent");
        selectedTileText.SetActive(true);
        selectedTileText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        selectedTileText.transform.localScale = Vector3.one;
        
        LeanTween.cancel(selectedTileText);
        LeanTween.rotateZ(selectedTileText, 10f, 0.5f).setEasePunch();
        LeanTween.scale(selectedTileText, new Vector3(1.3f, 1.3f), 0.5f).setEasePunch();
    }

    private void HeistOps_OnDeselectTween() {
        // Unselected Components
        var unselectedTileText = GameObject.Find("GridLayout/HeistOpsTile/Tile/TileTitle");
        unselectedTileText.SetActive(true);
        unselectedTileText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        unselectedTileText.transform.localScale = Vector3.one;

        LeanTween.cancel(unselectedTileText);
        LeanTween.rotateZ(unselectedTileText, 10f, 0.5f).setEasePunch();
        LeanTween.scale(unselectedTileText, new Vector3(1.3f, 1.3f), 0.5f).setEasePunch();
        
        // Selected Components
        var selectedContent = GameObject.Find("GridLayout/HeistOpsTile/Tile/SelectedContent");
        selectedContent.SetActive(false);
    }

    // IEnumerator UIDelay() {
    //     yield return new WaitForSeconds(1f);
    //     MainMenu_OpenAnimation();
    // }
}
