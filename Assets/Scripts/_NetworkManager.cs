using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class _NetworkManager : MonoBehaviourPunCallbacks
{
    //singleton
    public static _NetworkManager instance;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Realizar conexion con datos colocados en editor
    }


    /// <summary>
    /// Crear un nuevo Room con el nombre dado
    /// </summary>
    /// <param name="_name">Nombre que va tener el room</param>
    public void CreateRoom(string _name)
    {
        PhotonNetwork.CreateRoom(_name);
    }

    /// <summary>
    /// Verificacion si el room fue creado
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("Se creo room : " + PhotonNetwork.CurrentRoom.Name);
    }

    /// <summary>
    /// Unirse a room ya existente
    /// </summary>
    /// <param name="_name">Nombre del room al que se quiere unir</param>
    public void JoinRoom(string _name)
    {
        PhotonNetwork.JoinRoom(_name);
    }

    /// <summary>
    /// Abandonar el room Actual desde lobby
    /// </summary>
    public void LeveaRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Cargar una nueva escena usando Photon
    /// </summary>
    /// <param name="_nameScene">Nombre de la escena que se va cargar</param>
    [PunRPC]
    public void LoadScene(string _nameScene)
    {
        PhotonNetwork.LoadLevel(_nameScene);
    }

}
