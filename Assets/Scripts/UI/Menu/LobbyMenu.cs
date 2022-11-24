using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbyMenu : MonoBehaviour {
    //Loading of level selected
    public void OnClick_Play() {
        SceneManager.LoadScene("VrMission1");
        //MenuManager.Init();
    }
}
