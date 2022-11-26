using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class AIChasingMulti : MonoBehaviourPunCallbacks
{
    //Takes player position
    [SerializeField] public Transform player;
    //Takes enemy navMesh
    NavMeshAgent enemy;
    // Start is called before the first frame update
    void Start()
    {
        //Initializes enemy and blocks navMesh bugs
        enemy = GetComponent<NavMeshAgent>();
        enemy.updateRotation = false;
        enemy.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Determine path to follow for chase
        photonView.RPC("ChasePlayer", RpcTarget.All);
        
    }
    
    [PunRPC]
    private void ChasePlayer() {
        enemy.SetDestination(player.position);
        Debug.Log("Siguiendo");
    }
}