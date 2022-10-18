using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitController : MonoBehaviour
{
    public GameObject upbox;
    public GameObject downbox;
    public GameObject leftbox;
    public GameObject rightbox;
    public bool canHit=false;
    public float hitTime=1f;
    private MovementV2 movementV2;
    public float counter;

    // Start is called before the first frame update
    void Start()
    {
        movementV2 = GetComponent<MovementV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canHit)
        {
            
        if (movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Left && canHit) 
            {
                Debug.Log("pegue");
                upbox.SetActive(false);
                downbox.SetActive(false);
                leftbox.SetActive(true);
                rightbox.SetActive(false);

                counter += Time.deltaTime;

                if( counter > hitTime)
                {
                    Debug.Log("nopegue");
                    upbox.SetActive(false);
                    downbox.SetActive(false);
                    leftbox.SetActive(false);
                    rightbox.SetActive(false);
                    counter=0;
                    canHit=false;
                }
            }
        if (movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Down && canHit)
            {
                Debug.Log("pegue");
                upbox.SetActive(false);
                downbox.SetActive(true);
                leftbox.SetActive(false);
                rightbox.SetActive(false);

                counter += Time.deltaTime;

                if( counter > hitTime)
                {
                    Debug.Log("nopegue");
                    upbox.SetActive(false);
                    downbox.SetActive(false);
                    leftbox.SetActive(false);
                    rightbox.SetActive(false);
                    counter=0;
                    canHit=false;
                }
            }
        if (movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Up && canHit)
            {
                Debug.Log("pegue");
                upbox.SetActive(true);
                downbox.SetActive(false);
                leftbox.SetActive(false);
                rightbox.SetActive(false);

                counter += Time.deltaTime;

                if( counter > hitTime)
                {
                    Debug.Log("nopegue");
                    upbox.SetActive(false);
                    downbox.SetActive(false);
                    leftbox.SetActive(false);
                    rightbox.SetActive(false);
                    counter=0;
                    canHit=false;
                }
            }
        if (movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Right && canHit)
            {
                Debug.Log("pegue");
                upbox.SetActive(false);
                downbox.SetActive(false);
                leftbox.SetActive(false);
                rightbox.SetActive(true);

                counter += Time.deltaTime;

                if( counter > hitTime)
                {
                    Debug.Log("nopegue");
                    upbox.SetActive(false);
                    downbox.SetActive(false);
                    leftbox.SetActive(false);
                    rightbox.SetActive(false);
                    counter=0;
                    canHit=false;
                }
            }
        }
        
    }
    public void SetHit(bool hit =false)
    {
        canHit= hit;
        Debug.Log(canHit);
    }
}

