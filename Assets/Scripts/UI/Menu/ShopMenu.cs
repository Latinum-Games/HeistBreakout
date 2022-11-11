using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour {
    //Returns to main menu
    public void OnClick_Back() {
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }
}
