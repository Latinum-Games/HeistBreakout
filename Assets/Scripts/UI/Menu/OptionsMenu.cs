using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void OnClick_Graphics()
    {
        //load menu of graphics
    }

    public void OnClick_Controlls()
    {
        //load controlls 
    }

    public void OnClick_Sound()
    {
        //load sound
    }

    public void OnClick_Return()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }
}