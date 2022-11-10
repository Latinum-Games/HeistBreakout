using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class _GameController : MonoBehaviourPunCallbacks
{
    //Instancia
    public static _GameController instance;

    public bool isGameEnd = false; //Saber si el juego se completo
    public string playerPrefab; //Prefab de la carpeta de Recuersos

    public Transform[] spawnPlayerPositions; // posiciones donde se puede colocar los players
    public PlayerController[] players; //controlador de player

    private int playerInGame; //Numero de players en el room

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length]; // Inicializar el vector de jugadores
        photonView.RPC("InGame", RpcTarget.AllBuffered);// Colocar los players en una posicion de lista de spawner
    }


    [PunRPC]
    void InGame()
    {
        playerInGame++; // contador de jugadores
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();// mandar llamar posicionamiento de player
        }
    }

    void SpawnPlayer()
    {
        int randomPosition = Random.Range(0, spawnPlayerPositions.Length);//Obtener posicion random de lista de posiciones
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab, spawnPlayerPositions[randomPosition].position, Quaternion.identity);// instanciar el player en una posiciocion aleatoria


        PlayerController playScript = playerObj.GetComponent<PlayerController>();// Obtener script que controla al jugador
        playScript.photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer); // Mandar ejecutar funcion de inicializador de player
    }
}
