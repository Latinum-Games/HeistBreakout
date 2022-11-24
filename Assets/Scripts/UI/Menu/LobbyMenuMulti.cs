using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbyMenuMulti : MonoBehaviourPunCallbacks {
    [PunRPC]
    //Loading of level selected
    public void OnClick_Play() {
        if (SceneManager.GetActiveScene().name == "UI Heist Breakout") {
            SceneManager.LoadScene("SceneMainMenu");
        }else if (SceneManager.GetActiveScene().name == "VrMissionMulti1") {
            _NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, "VrMissionMulti1");
            //MenuManager.Init();
        }
    }
}
