using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbyMenu : MonoBehaviour {
    public void OnClick_Play() {
        SceneManager.LoadScene("TutorialLevel");
        MenuManager.Init();
    }
}
