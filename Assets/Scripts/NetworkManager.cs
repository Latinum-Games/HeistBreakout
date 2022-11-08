using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        {
            if (instance != null && instance != this)
            {
                gameObject.SetActive(false);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Conectado");
        CreateRoom("TestRoom");
    }
    public void CreateRoom(string _name)//nombre de variables para solo funciones o publicas
    {
        PhotonNetwork.CreateRoom(_name);
    }
    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        Debug.Log("se creo room " + PhotonNetwork.CurrentRoom.Name);
    }
    public void JoinRoom(string _name)
    {
        PhotonNetwork.JoinRoom(_name);
    }
    public void JoinScene(string _name)
    {
        PhotonNetwork.LoadLevel(_name);
    }

}
