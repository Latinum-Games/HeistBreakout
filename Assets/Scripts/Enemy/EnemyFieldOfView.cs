using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyFieldOfView : MonoBehaviour {
    
    // Custom Event
    //Initializes based on detection
    [Header("Events")]
    public UnityEvent onDetection;
    public UnityEvent onChasing;
    public UnityEvent onLost;

    //Initializes timers based for see time and changes in view
    [Header("Timers")]
    public float seeTime= 5f;
    public float timer=0f;
    public float timeChangeView = 2f;
    
    //Initializes the view range limit
    [Header("View range properties")]
    public float radius = 5f;
    [Range(1,360)]public float angle =90f;

    //Initializes the layers 
    [Header("Layers")]
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    //Initializes the player reference
    [Header("Player reference")]
    public GameObject playerRef;

    //Enemy properties
    //Initializes states, direction angles, velocity, position, etc
    [SerializeField] public bool CanSeePlayer {get; private set;}
    [SerializeField] private Vector2 directionToTarget {get; set;}
    [SerializeField] private float directionAngleHort {get; set;}
    [SerializeField] private float directionAngleVert {get; set;}
    [SerializeField] private bool noFirst {get; set;}
    
    [Header("Enemy properties")]
    [SerializeField] private bool CanWalk;
    [SerializeField] public Vector2 enemyVel;
    [SerializeField] private Vector2 prevPos;
    
    //Initializes the enemy states for attention
    public enum EnemyState {
        Patrolling,
        Alert,
        Persecution,
        Hit
    }

    //Initializes the enemy see state
    private enum ActiveEnemyAngleState {
        Left,
        Right, 
        Up, 
        Down
    }
    
    //Initial states for seeing up and patrolling
    [SerializeField] private ActiveEnemyAngleState enemyAngleState = ActiveEnemyAngleState.Up;
    [SerializeField] public EnemyState State = EnemyState.Patrolling;

    //Initializes movement mechanics for enemy based in view
    [Header("Movement scripts AI")]
    private AIChasing scriptChasing;
    private WaypointMover scriptWaypoint;

    //Initializes hitboxes direction for enemy visual indicators
    [Header("Hitboxes ")]
    public GameObject upHit;
    public GameObject downHit;
    public GameObject leftHit;
    public GameObject rightHit;
    
    // Start is called before the first frame update
    void Start()
    {
        //Takes AI chasing script and disables it
        scriptChasing = GetComponent<AIChasing>();
        scriptChasing.enabled = false;
        //Takes player reference gameobject
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //Takes Waypointer script and enables it
        scriptWaypoint = GetComponent<WaypointMover>();
        scriptWaypoint.enabled = true;
        
    }

    void FixedUpdate() {
        
        //Calculates enemy velocity based in the previous position
        enemyVel = (new Vector2 (transform.position.x - prevPos[0], transform.position.y- prevPos[1]))/Time.deltaTime;
        prevPos = new Vector2 (transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Look constantly O-O
        FOV();
        
        //Calculates the magnitude of enemy velocity and calculates the direction of movement
        var velMagn = enemyVel.magnitude;
        enemyVel = enemyVel.normalized;
        var fwdDotProduct = Vector2.Dot(transform.up, enemyVel);
        var rightDotProduct = Vector2.Dot(transform.right, enemyVel);

        
        //Changes where the enemy sees depending on his movement
        if (rightDotProduct > fwdDotProduct)
        {
            if (rightDotProduct > 0)
            {
                enemyAngleState = ActiveEnemyAngleState.Right;
            }else{
                enemyAngleState = ActiveEnemyAngleState.Left;
            }
        }else{
            if (fwdDotProduct > 0)
            {
                enemyAngleState = ActiveEnemyAngleState.Up;
            }else{
                enemyAngleState = ActiveEnemyAngleState.Down;
            }
        }
        
        //STATE: Patrolling
        if (State == EnemyState.Patrolling)
        {
            timer += Time.deltaTime;
            if (timer > timeChangeView)
            {
                timer = 0;
            }
            
            //When seen
            if(CanSeePlayer)
            {
                State = EnemyState.Alert;
            }
        }
        
        //STATE: Alert
        //If enemy sees x seconds the player, this will start chasing
        if (State == EnemyState.Alert) {
            if (CanSeePlayer)
            {
                timer += Time.deltaTime;
                if (timer>=seeTime)
                {
                    onDetection.Invoke();
                    scriptWaypoint.enabled = false;
                    scriptChasing.enabled = true;
                    State= EnemyState.Persecution;
                    timer=0;
                }
            }
            
            //If enemy has lost sight of player,this will stop chasing
            if (!CanSeePlayer) {

                timer += Time.deltaTime;

                if (timer>=seeTime)
                {
                    onLost.Invoke();
                    scriptChasing.enabled = false;
                    scriptWaypoint.enabled = true;  
                    State= EnemyState.Patrolling;
                    GetComponent<WaypointMover>().canWalk=true;
                    timer=0;
                }
            }
        }

        //STATE: Persecution
        //Enemy is chasing the player and stops patrolling
        if (State == EnemyState.Persecution) {
            if (CanSeePlayer)
            {
                onDetection.Invoke();
                GetComponent<WaypointMover>().canWalk=false;
                scriptWaypoint.enabled = false;
                scriptChasing.enabled = true;
            }
            //Enemy lost sight of player when chasing
            if (!CanSeePlayer)
            {
                timer += Time.deltaTime;
                if (timer>=seeTime) {
                    
                    onChasing.Invoke();
                    scriptWaypoint.enabled = false;
                    scriptChasing.enabled = true;

                    State= EnemyState.Alert;
                    timer=0;
                }
            }
        }
    }

    //Field of view primary function
    //Contains all the logic for the view and control states
    private void FOV()
    {
        //Generation of a circle checker for the player
        Collider2D[] rangeCheck =Physics2D.OverlapCircleAll(transform.position,radius,targetLayer); 

        //Revision of the player in the circle of the enemy
        if(rangeCheck.Length>0)
        {
            Transform target = rangeCheck[0].transform;
            directionToTarget=(target.position-transform.position).normalized;
            directionAngleVert = Vector2.Angle(transform.up,directionToTarget);
            directionAngleHort = Vector2.Angle(transform.right,directionToTarget);
            float distanceToTarget = Vector2.Distance(transform.position,target.position);

            //Revision of player seen without obstacles
            if(!Physics2D.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionLayer))
            {
                //Revision of player entering in the active angle of the enemy
                if (!noFirst){
                    if (enemyAngleState == ActiveEnemyAngleState.Right && directionAngleHort<angle/2){
                        CanSeePlayer = true;
                    }else if (enemyAngleState == ActiveEnemyAngleState.Down && directionAngleVert > angle*1.5){
                        CanSeePlayer = true;
                    }else if (enemyAngleState == ActiveEnemyAngleState.Left && directionAngleHort>angle*1.5){
                        CanSeePlayer = true;
                    }else if (enemyAngleState == ActiveEnemyAngleState.Up && directionAngleVert<angle/2){
                        CanSeePlayer = true;
                    }else{
                        CanSeePlayer = false;
                    }
                    noFirst = true;
                }
            }else{
                CanSeePlayer = false;
            }

            //If player seen by enemy and is still at the range of view, enemy will follow it with the view
            if(CanSeePlayer){
                if(directionAngleVert<angle/2){
                    enemyAngleState = ActiveEnemyAngleState.Up;

                }else if (directionAngleVert > angle*1.5){
                    enemyAngleState = ActiveEnemyAngleState.Down;

                }else if(directionAngleHort<angle/2){
                    enemyAngleState = ActiveEnemyAngleState.Right;

                }else if(directionAngleHort>angle*1.5){
                    enemyAngleState = ActiveEnemyAngleState.Left;

                }else{
                    noFirst = false;
                }
            }else{
                noFirst = false;
            }

        }else{
            CanSeePlayer=false;
        }

        //Visual indicator of the active angle of the enemy
        SetActiveHitBox(enemyAngleState);
        
    }
    //Sets the active box depending on the enemy look state
    private void SetActiveHitBox(ActiveEnemyAngleState activeHitbox) {
        upHit.SetActive(false);
        downHit.SetActive(false);
        leftHit.SetActive(false);
        rightHit.SetActive(false);
        
        if (activeHitbox == ActiveEnemyAngleState.Left) {
            leftHit.SetActive(true);
        } else if (activeHitbox == ActiveEnemyAngleState.Down) {
            downHit.SetActive(true);
        } else if (activeHitbox == ActiveEnemyAngleState.Right) {
            rightHit.SetActive(true);
        }else if (activeHitbox == ActiveEnemyAngleState.Up) {
            upHit.SetActive(true);
        }
    }

    //Change of view in the enemy depending on the active angle state view
    public Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
            
        if (enemyAngleState == ActiveEnemyAngleState.Right)
        {
            return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
                
        }else if (enemyAngleState == ActiveEnemyAngleState.Up){
            
            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

        }else if (enemyAngleState == ActiveEnemyAngleState.Left){
            
            return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));

        }else if (enemyAngleState == ActiveEnemyAngleState.Down){
            
            return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
}

//Editor indicators for the enemy view angles and seen player
#if UNITY_EDITOR
[CustomEditor(typeof(EnemyFieldOfView))]
public class HandlesDemoEditor : Editor 
{
    public void OnSceneGUI() 
    {
        var linkedObject = target as EnemyFieldOfView;

        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(linkedObject.transform.position,Vector3.forward,linkedObject.radius);

        Vector3 angle01 = linkedObject.DirectionFromAngle(-linkedObject.transform.eulerAngles.z,-linkedObject.angle/2);
        Vector3 angle02 = linkedObject.DirectionFromAngle(linkedObject.transform.eulerAngles.z,linkedObject.angle/2);
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawLine(linkedObject.transform.position,linkedObject.transform.position + angle01 * linkedObject.radius);
        UnityEditor.Handles.DrawLine(linkedObject.transform.position,linkedObject.transform.position + angle02 * linkedObject.radius);

        if(linkedObject.CanSeePlayer)
        {
            UnityEditor.Handles.color =Color.green;
            UnityEditor.Handles.DrawLine(linkedObject.transform.position,linkedObject.playerRef.transform.position);
        }
    }
}

#endif