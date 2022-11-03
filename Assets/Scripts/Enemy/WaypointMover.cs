using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class handles moving the attached game object between waypoints
/// </summary>
public class WaypointMover : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("A list of transforms to move between")]
    public List<Transform> waypoints;
    [Tooltip("How fast to move the platform")]
    public float moveSpeed = 1f;
    [Tooltip("How long to wait when arriving at a waypoint")]
    public float waitTime = 3f;

    // The time at which movement is resumed
    private float timeToStartMovingAgain = 0f;
    // Whether or not the waypoint mover is stopped
    [HideInInspector] 
    public bool stopped = false;

    // The previous waypoint or the starting position
    private Vector3 previousTarget;

    private Vector3 currentTarget;

    private int currentTargetIndex;
    public bool canWalk=true;

    NavMeshAgent enemy;

    [HideInInspector] 
    public Vector3 travelDirection;


    void Start()
    {
        InitializeInformation();
        
    }


    void Update()
    {
        if(canWalk)
        {
            ProcessMovementState();
        }
        
    }


    void ProcessMovementState()
    {
        if (stopped)
        {
            StartCheck();
        }
        else
        {
            Travel();
        }
    }


    void StartCheck()
    {
        if (Time.time >= timeToStartMovingAgain)
        {
            stopped = false;
            previousTarget = currentTarget;
            currentTargetIndex += 1;
            if (currentTargetIndex >= waypoints.Count)
            {
                currentTargetIndex = 0;
            }
            currentTarget = waypoints[currentTargetIndex].position;
            CalculateTravelInformation();
        }
    }


    void InitializeInformation()
    {

        enemy = GetComponent<NavMeshAgent>();
        enemy.updateRotation = false;
        enemy.updateUpAxis = false;


        previousTarget = this.transform.position;
        currentTargetIndex = 0;
        if (waypoints.Count > 0)
        {
            currentTarget = waypoints[0].position;
        }
        else
        {
            waypoints.Add(this.transform);        
            currentTarget = previousTarget;
        }
        
        CalculateTravelInformation();
    }


    void CalculateTravelInformation()
    {
        travelDirection = (currentTarget - previousTarget).normalized;
    }


    void Travel()
    {
        transform.Translate(travelDirection * moveSpeed * Time.deltaTime);
        bool overX = false;
        bool overY = false;
        bool overZ = false;

        Vector3 directionFromCurrentPositionToTarget = currentTarget - transform.position;

        if (directionFromCurrentPositionToTarget.x == 0 || Mathf.Sign(directionFromCurrentPositionToTarget.x) != Mathf.Sign(travelDirection.x))
        {
            overX = true;
            // Debug.Log("uwu overX");
        }
        if (directionFromCurrentPositionToTarget.y == 0 || Mathf.Sign(directionFromCurrentPositionToTarget.y) != Mathf.Sign(travelDirection.y))
        {
            overY = true;
            // Debug.Log("uwu overY");
        }
        if (directionFromCurrentPositionToTarget.z == 0 || Mathf.Sign(directionFromCurrentPositionToTarget.z) != Mathf.Sign(travelDirection.z))
        {
            overZ = true;
            // Debug.Log("uwu overZ");
        }

        enemy.SetDestination(new Vector3(currentTarget.x, currentTarget.y, currentTarget.z));

        if (overX && overY || overZ)
        {
            BeginWait();
        }
    }


    void BeginWait()
    {
        stopped = true;
        timeToStartMovingAgain = Time.time + waitTime;
        Debug.Log("uwu waiting");
    }
}
