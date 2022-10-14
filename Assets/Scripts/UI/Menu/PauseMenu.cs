using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void OnClick_Resume()
    {
        //quit menu
        //Implementacion con events con Charlie
    }

    public void OnClick_Options()
    {
        MenuManager.OpenMenu(Menu.OPTIONS_MENU, gameObject);
    }

    public void OnClick_Exit()
    {
        MenuManager.OpenMenu(Menu.EXIT_MENU, gameObject);
    }
}