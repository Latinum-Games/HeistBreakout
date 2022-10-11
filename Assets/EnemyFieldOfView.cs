using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public float radius = 5f;
    public float seeTime=5f;
    public float timer=0f;
    [Range(1,360)]private float angle =90f;
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

    [SerializeField] private ActiveEnemyAngleState enemyAngleState = ActiveEnemyAngleState.Down;
    [SerializeField] private EnemyState State = EnemyState.Patrolling;
    
    // Start is called before the first frame update
    void Start()
    {
        scriptChasing = GetComponent<AIChasing>();
        scriptChasing.enabled = false;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        
        //StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        //MIRAR CONSTANTEMENTE 0-0
        FOV();
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
            
            scriptChasing.enabled = true;
            State= EnemyState.Persecution;
            
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
            scriptChasing.enabled = true;
        }


        //YA  NO TE VEO Y TE PERSEGUIA
        if (!CanSeePlayer && State==EnemyState.Persecution)
        {
            scriptChasing.enabled = false;
            Debug.Log("te perseguia y ya no te veo");
            timer += Time.deltaTime;
            if (timer>=seeTime)
            {
            
            
            State= EnemyState.Alert;
            
            }
            else{
                //timer=0;
            }

            
        }

        //YA NO TE VEO Y ESTABA OBSERVANDO
        if (!CanSeePlayer && State==EnemyState.Alert)
        {
            Debug.Log("te veia y ya no te veo");
            scriptChasing.enabled = false;

            timer += Time.deltaTime;

            if (timer>=seeTime)
            {
           
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
                    }else if (enemyAngleState == ActiveEnemyAngleState.Up && directionAngleVert > angle*1.5){
                        CanSeePlayer = true;
                    }else if (enemyAngleState == ActiveEnemyAngleState.Left && directionAngleHort>angle*1.5){
                        CanSeePlayer = true;
                    }else if (enemyAngleState == ActiveEnemyAngleState.Down && directionAngleVert<angle/2){
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
                    enemyAngleState = ActiveEnemyAngleState.Down;

                }else if (directionAngleVert > angle*1.5){
                    enemyAngleState = ActiveEnemyAngleState.Up;

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
        }

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
        private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
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
