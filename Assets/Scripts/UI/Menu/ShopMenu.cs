using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour {
    
    [Header("Navigation")] 
    [SerializeField] private GameObject pullButtonParent;
    [SerializeField] private GameObject pullButtonPositionIn;
    [SerializeField] private GameObject pullButtonPositionOut;
    
    [SerializeField] private GameObject backButtonParent;
    [SerializeField] private GameObject backButtonPositionIn;
    [SerializeField] private GameObject backButtonPositionOut;

    [Header("Gacha Component")] 
    public Gachapon gachapon;

    private void Start() {
        // Setup Positions
        pullButtonParent.transform.position = pullButtonPositionOut.transform.position;
        backButtonParent.transform.position = backButtonPositionOut.transform.position;
        
        SetButtonListeners();
    }

    // ENABLE CUANDO SE ABRE LA VENTANA
    private void OnEnable() {
        Debug.Log("ON ENABLE DEL SHOP");
        ShopMenu_OpenAnimation();
    }

    // SETEAR LISTENERS DE LOS BOTONES
    private void SetButtonListeners() {
        var pullButton = pullButtonParent.GetComponent<Button>();
        var pullButtonEventHandlers = pullButtonParent.GetComponent<ButtonEventHandlers>();
        var backButton = backButtonParent.GetComponent<Button>();
        
        // Pull Button
        pullButton.onClick.AddListener(gachapon.GachaAnimation);
        
        // On Select Hover Animation
        pullButtonEventHandlers.onSelectAction.AddListener(() => {
            LeanTween.cancel(pullButtonParent);
            LeanTween.scale(pullButtonParent, new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseOutExpo();
        });
        
        // On Deselect Hover Animation
        pullButtonEventHandlers.onDeselectAction.AddListener(() => {
            LeanTween.cancel(pullButtonParent);
            LeanTween.scale(pullButtonParent, new Vector3(1.0f, 1.0f, 1.0f), 0.2f).setEaseOutExpo();
        });
        
        // Back Button
        backButton.onClick.AddListener(OnClick_Back);
    }
    
    //Returns to main menu
    public void OnClick_Back() {
        ShopMenu_CloseAnimation(() => { MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject); });
    }

    public void ShopMenu_OpenAnimation() {
        // TRANSFORM DE ENTRADA DE BOTONES
        LeanTween.cancel(pullButtonParent);
        LeanTween.move(pullButtonParent, pullButtonPositionIn.transform.position, 0.5f).setEaseOutExpo();
        
        LeanTween.cancel(backButtonParent);
        LeanTween.move(backButtonParent, backButtonPositionIn.transform.position, 0.5f).setEaseOutExpo();
    }

    public void ShopMenu_CloseAnimation(Action action = null) {
        // TRANSFORM DE SALIDA DE BOTONES
        LeanTween.delayedCall(backButtonParent, 0.5f, () => {
            LeanTween.cancel(pullButtonParent);
            LeanTween.move(pullButtonParent, pullButtonPositionOut.transform.position, 0.5f).setEaseOutExpo();
        });
        
        LeanTween.delayedCall(backButtonParent, 0.5f, () => {
            LeanTween.cancel(backButtonParent);
            LeanTween.move(backButtonParent, backButtonPositionOut.transform.position, 0.5f).setEaseOutExpo().setOnComplete(action);
        });
    }
}
