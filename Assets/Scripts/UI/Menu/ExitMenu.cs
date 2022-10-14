using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public void OnClick_Yes()
    {
        Application.Quit();
    }

    public void OnClick_No()
    {
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }

}
