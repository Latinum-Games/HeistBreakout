using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuUIController : MonoBehaviourPunCallbacks
{
    public GameObject mainWindow;//Objeto que contiene ventana principal
    public GameObject lobbyWidow; // Objeto que contiene lobby

    [Header("Menu Principal")]
    public Button createRoomBtn; // Boton de crear room
    public Button joinRoomBtn; //Boton de unirse a room

    [Header("Lobby")]
    public Button startGameBtn; // Boton de iniciar partida
    public TextMeshProUGUI playertextList; //Texto para imprimir jugadores en room
    public TextMeshProUGUI playerRoom;
    public TextMeshProUGUI roomCode;
    private string roomCodeStr = "";


    /// <summary>
    /// Verificar si se establecio la conexion
    /// </summary>
    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true; // Activar boton de crear room
        joinRoomBtn.interactable = true; //Activar botton de unirse a room
    }
    /// <summary>
    /// Obtener nickname de jugador desde input
    /// </summary>
    /// <param name="_playerName"> input donde se puede editar el nombre</param>
    public void GetPlayerName(TMP_InputField _playerName)
    {
        PhotonNetwork.NickName = _playerName.text; // Asignar el nombre de player en photon
    }

    /// <summary>
    /// Permite unirse a un room Existente
    /// </summary>
    /// <param name="_roomName">input donde se lee el nombre de room </param>
    public void JoinRoom(TMP_InputField _roomName)
    {
        _NetworkManager.instance.JoinRoom(_roomName.text);//Conectar a manager de conexion y enviar nombre de room
    }

    /// <summary>
    /// Crear un nuevo room
    /// </summary>
    /// <param name="_roomName"> Input de nombre que va recibir el room</param>
    public void CreateRoom(TMP_InputField _roomName)
    {
        _NetworkManager.instance.CreateRoom(_roomName.text); //Crear nuevo room desde networkManager
        roomCodeStr = _roomName.text;
    }

    /// <summary>
    /// Verificar si ya se unio el player a un room
    /// </summary>
    public override void OnJoinedRoom()
    {
        lobbyWidow.SetActive(true);
        mainWindow.SetActive(false);
        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }
    /// <summary>
    /// Actualizar la informacion dentro de lobby 
    /// </summary>
    [PunRPC]
    public void UpdatePlayerInfo()
    {
        playertextList.text = ""; //Limpiar campo de texto
        foreach (Player player in PhotonNetwork.PlayerList) //Ciclo para pintar todos los player en el Room
        {
            playertextList.text += player.NickName + "\n";//Agregar nombre de players
        }


        if (PhotonNetwork.IsMasterClient) //Verificar si el cliente es Master Clien
        {
            startGameBtn.interactable = true;//Activar boton de iniciar juego
        }
        else
        {
            startGameBtn.interactable = false; // Desactivar boton de inicio de juego
        }

        playerRoom.text = PhotonNetwork.PlayerList[0].NickName + "'s Lobby";
        roomCode.text = "Room code: " + roomCodeStr;
    }

    /// <summary>
    /// Salir del room actual
    /// </summary>
    public void LeaveLobby()
    {
        lobbyWidow.SetActive(false); //Ocultar ventant de lobby
        mainWindow.SetActive(true);//Activar ventana de main

        _NetworkManager.instance.LeveaRoom();// ejecurtar funcion de salir de room
        UpdatePlayerInfo();//Actualizar la informacion
    }

    /// <summary>
    /// Abrir nueva escena  con el juego
    /// </summary>
    public void StartGame()
    {
        _NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, "VrMissionMulti1");
    }
}
