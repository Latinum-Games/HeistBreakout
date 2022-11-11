using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public int id;
    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;


    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Jump();
        }

    }
    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);
    }
    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 0.7f))
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// Inicializar la informacion del player actual
    /// </summary>
    /// <param name="player"> Data de player</param>
    [PunRPC]
    public void Init(Player player)
    {
        photonPlayer = player;// Asiganar el player actual
        id = player.ActorNumber;//Guardar el id del player
        _GameController.instance.players[id - 1] = this;// Asiganarlo a las lista de player dentro del game controller

        if (!photonView.IsMine) // Verificar si el movimiento es del usuario actual
        {
            rig.isKinematic = true;
        }
    }

    //Validar una collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("door"))//Verificar si coincide con el tag
        {
            photonView.RPC("Hidewall", RpcTarget.All, collision.gameObject.GetComponent<PhotonView>().ViewID);//Actualizar la informacion del objeto en todos las sesiones
        }
    }

    /// <summary>
    /// Ocultar el objeto desde su Id de PhotonView
    /// </summary>
    /// <param name="_id">Id del objeto</param>
    [PunRPC]
    void Hidewall(int _id)
    {
        GameObject obj = PhotonNetwork.GetPhotonView(_id).gameObject;//Obtener el Game object desde photon
        obj.gameObject.SetActive(false); //Desactivar el elemento
    }


}
