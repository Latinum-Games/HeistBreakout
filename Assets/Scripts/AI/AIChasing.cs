using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChasing : MonoBehaviour
{
    //Takes player position
    [SerializeField] Transform player;
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
        enemy.SetDestination(player.position);
    }
}