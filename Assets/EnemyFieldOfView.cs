using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyFieldOfView : MonoBehaviour
{
    public float radius = 5f;
    public float seeTime=5f;
    public float timer=0f;
    public float timeChangeView = 2f;
    [Range(1,360)]public float angle =90f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject playerRef;
    public bool CanSeePlayer {get; private set;}
    private Vector2 directionToTarget {get; set;}
    private float directionAngleHort {get; set;}
    private float directionAngleVert {get; set;}
    private bool noFirst {get; set;}
    private bool CanWalk;

    private AIChasing scriptChasing;
    private WaypointMover scriptWaypoint;

    private Vector2 enemyVel;
    private Vector2 prevPos;

    [Header("Hitboxes")]
    public GameObject upHit;
    public GameObject downHit;
    public GameObject leftHit;
    public GameObject rightHit;

    public enum EnemyState {
        Patrolling,
        Alert,
        Persecution,
        Hit
    }

    private enum ActiveEnemyAngleState {
        Left,
        Right, 
        Up, 
        Down
    }

    [SerializeField] private ActiveEnemyAngleState enemyAngleState = ActiveEnemyAngleState.Up;
    [SerializeField] private EnemyState State = EnemyState.Patrolling;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptChasing = GetComponent<AIChasing>();
        scriptChasing.enabled = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");

        scriptWaypoint = GetComponent<WaypointMover>();
        scriptWaypoint.enabled = true;

        
        //StartCoroutine(FOVCheck());
    }

    void FixedUpdate() {
        enemyVel = (new Vector2 (transform.position.x - prevPos[0], transform.position.y- prevPos[1]))/Time.deltaTime;
        prevPos = new Vector2 (transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //MIRAR CONSTANTEMENTE 0-0
        FOV();

        var velMagn = enemyVel.magnitude;
        enemyVel = enemyVel.normalized;
        var fwdDotProduct = Vector2.Dot(transform.up, enemyVel);
        var rightDotProduct = Vector2.Dot(transform.right, enemyVel);
        
        //print(fwdDotProduct);
        //print(rightDotProduct);
        if(fwdDotProduct > 0){
            print("I am moving forward");
        }
        else if(fwdDotProduct < 0){
            print("I am moving backward");
        }
        if(rightDotProduct < 0){
            print("I am moving left");
        }
        else if(rightDotProduct > 0){
            print("I am moving right");
        }

        
        if (State == EnemyState.Patrolling)
        {
            timer += Time.deltaTime;
            if (timer > timeChangeView)
            {
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
                timer = 0;
            }
            
        }

        //TE VI
        if(CanSeePlayer && State==EnemyState.Patrolling)
        {
            State= EnemyState.Alert;
        }



        //TE OBSERVO 5 SEG
        if (CanSeePlayer && State==EnemyState.Alert)
        {
            timer += Time.deltaTime;
            if (timer>=seeTime)
            {Debug.Log("te vi 5 seg hehe te persiguire");
            
            scriptWaypoint.enabled = false;
            scriptChasing.enabled = true;
            State= EnemyState.Persecution;
            timer=0;
            }
            else{
                //timer=0;
            }
        }


        //TE OBSERVE 5 SEG Y TE SEGUIRE
        if (CanSeePlayer && State==EnemyState.Persecution)
        {
            GetComponent<WaypointMover>().canWalk=false;
            Debug.Log("te estoy persiguiendo jeje");
            scriptWaypoint.enabled = false;
            scriptChasing.enabled = true;
        }


        //YA  NO TE VEO Y TE PERSEGUIA
        if (!CanSeePlayer && State==EnemyState.Persecution)
        {
            
            Debug.Log("te perseguia y ya no te veo");
            timer += Time.deltaTime;
            if (timer>=seeTime)
            {
            
            scriptWaypoint.enabled = false;
            scriptChasing.enabled = true;

            State= EnemyState.Alert;
            timer=0;
            }
            else{
                //timer=0;
            }

            
        }

        //YA NO TE VEO Y ESTABA OBSERVANDO
        if (!CanSeePlayer && State==EnemyState.Alert)
        {
            Debug.Log("te veia y ya no te veo");
            

            timer += Time.deltaTime;

            if (timer>=seeTime)
            {
            scriptChasing.enabled = false;
            scriptWaypoint.enabled = true;  
            State= EnemyState.Patrolling;
            GetComponent<WaypointMover>().canWalk=true;
            timer=0;
            }
            else{
                //timer=0;
            }

        }



    }
    //private IEnumerator FOVCheck()
    //{
        //WaitForSeconds wait =new WaitForSeconds(0.2f);
        //while(true)
        //{
            //yield return wait;
          //  FOV();
        //}
    //}
    private void FOV()
    {
        Collider2D[] rangeCheck =Physics2D.OverlapCircleAll(transform.position,radius,targetLayer); 

        if(rangeCheck.Length>0)
        {
            Transform target = rangeCheck[0].transform;
            directionToTarget=(target.position-transform.position).normalized;
            directionAngleVert = Vector2.Angle(transform.up,directionToTarget);
            directionAngleHort = Vector2.Angle(transform.right,directionToTarget);
            float distanceToTarget = Vector2.Distance(transform.position,target.position);

            if(!Physics2D.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionLayer))
            {
                //revisar que se entre desde le angulo que esta viendo el enemigo
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

            //si ya se vio y se mueve dentro del rango de visión del enemigo, el enemigo lo seguirá con la vista
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

            if(enemyAngleState == ActiveEnemyAngleState.Up)
            {
                upHit.SetActive(true);
                downHit.SetActive(false);
                leftHit.SetActive(false);
                rightHit.SetActive(false);
            }else if(enemyAngleState == ActiveEnemyAngleState.Left)
            {
                upHit.SetActive(false);
                downHit.SetActive(false);
                leftHit.SetActive(true);
                rightHit.SetActive(false);
            }else if(enemyAngleState == ActiveEnemyAngleState.Down)
            {
                upHit.SetActive(false);
                downHit.SetActive(true);
                leftHit.SetActive(false);
                rightHit.SetActive(false);
            }else if(enemyAngleState == ActiveEnemyAngleState.Right)
            {
                upHit.SetActive(false);
                downHit.SetActive(false);
                leftHit.SetActive(false);
                rightHit.SetActive(true);
            }

        }

/*
        private void OnDrawGizmos() {
            
                Gizmos.color=Color.white;
                #if UNITY_EDITOR
                UnityEditor.Handles.DrawWireDisc(transform.position,Vector3.forward,radius);
                #endif
                
                Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z,-angle/2);
                Vector3 angle02 = DirectionFromAngle(transform.eulerAngles.z,angle/2);
                Gizmos.color =Color.yellow;
                Gizmos.DrawLine(transform.position,transform.position + angle01 * radius);
                Gizmos.DrawLine(transform.position,transform.position + angle02 * radius);

                if(CanSeePlayer)
                {
                    Gizmos.color =Color.green;
                    Gizmos.DrawLine(transform.position,playerRef.transform.position);
                    
                }

            
        }
        */

        public Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            
            if (enemyAngleState == ActiveEnemyAngleState.Right)
            {
                //derecha
                return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
                
            }else if (enemyAngleState == ActiveEnemyAngleState.Up){

                //arriba
                return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

            }else if (enemyAngleState == ActiveEnemyAngleState.Left){

                //izquierda
                return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));

            }else if (enemyAngleState == ActiveEnemyAngleState.Down){

                //abajo
                return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

            }
                return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

            }

                
        }

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


            
        
