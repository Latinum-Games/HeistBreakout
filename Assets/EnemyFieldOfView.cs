using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1,360)]public float angle =45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public GameObject playerRef;
    public bool CanSeePlayer {get; private set;}
    private Vector2 directionToTarget {get; set;}
    
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait =new WaitForSeconds(0.2f);
        while(true)
        {
            yield return wait;
            FOV();
        }
    }
    private void FOV()
    {
        Collider2D[] rangeCheck =Physics2D.OverlapCircleAll(transform.position,radius,targetLayer);
        if(rangeCheck.Length>0)
        {
            Transform target = rangeCheck[0].transform;
            directionToTarget=(target.position-transform.position).normalized;
            if(Vector2.Angle(transform.up,directionToTarget)<angle/2)
            {
                float distanceToTarget = Vector2.Distance(transform.position,target.position);

                if(!Physics2D.Raycast(transform.position,directionToTarget,distanceToTarget,obstructionLayer))
                CanSeePlayer=true;
                else
                CanSeePlayer=false;
            }
            else
            CanSeePlayer= false;

            }
            else if (CanSeePlayer)
            CanSeePlayer=false;
        }
        private void OnDrawGizmos() {
            
                Gizmos.color=Color.white;
                UnityEditor.Handles.DrawWireDisc(transform.position,Vector3.forward,radius);
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
            Debug.Log(directionToTarget);
            //Debug.Log(directionToTarget[0]);
            
            if (directionToTarget[0] >= 0.5f && directionToTarget[1] >= -0.5f && directionToTarget[1] <= 0.5f)
            {
                //derecha
                return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
                
            }else if (directionToTarget[1] >= 0.5f && directionToTarget[0] >= -0.5f && directionToTarget[0] <= 0.5f){

                //arriba
                return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));

            }else if (directionToTarget[0] <= -0.5f && directionToTarget[1] <= -0.5f && directionToTarget[1] <= 0.5f){

                //izquierda
                return new Vector2(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

            }else if (directionToTarget[0] > 0 && directionToTarget[1] < 0){

                return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
            }
            
                
                return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
        }

    }

