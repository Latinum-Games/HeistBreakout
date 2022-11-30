using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitControllerMulti : MonoBehaviour
{
    //Initializes hitboxes from all directions
    public GameObject upbox;
    public GameObject downbox;
    public GameObject leftbox;
    public GameObject rightbox;
    //Initializes changes for attack hitboxes based on movement controller
    public bool canHit=false;
    public float hitTime=1f;
    private MovementV2Multi movementV2Multi;
    public float counter;
    private MovementV2Multi.PlayerLookState PlayerLookState;

    // Start is called before the first frame update
    void Start()
    {
        //Gets movement controller
        movementV2Multi = GetComponent<MovementV2Multi>();
    }

    // Update is called once per frame
    void Update()
    {
        //Setter for active hitboxes depending on player look state
        if(canHit) {
            PlayerLookState = movementV2Multi.GetPlayerLookState();
            Debug.Log("pegue [hitController]");
            SetActiveHitBox(PlayerLookState);
            
            counter += Time.deltaTime;

            if (counter > hitTime) {
                Debug.Log("nopegue [hitController]");
                SetActiveHitBox();
                counter = 0;
                canHit = false;
            }
        }
    }
    
    //Sets the active box null 
    private void SetActiveHitBox() {
        upbox.SetActive(false);
        downbox.SetActive(false);
        leftbox.SetActive(false);
        rightbox.SetActive(false);
    }
    
    //Sets the active box depending on the player look state
    private void SetActiveHitBox(MovementV2Multi.PlayerLookState activeHitbox) {
        SetActiveHitBox();
        
        if (activeHitbox == MovementV2Multi.PlayerLookState.Left) {
            leftbox.SetActive(true);
        } else if (activeHitbox == MovementV2Multi.PlayerLookState.Down) {
            downbox.SetActive(true);
        } else if (activeHitbox == MovementV2Multi.PlayerLookState.Right) {
            rightbox.SetActive(true);
        }else if (activeHitbox == MovementV2Multi.PlayerLookState.Up) {
            upbox.SetActive(true);
        }
    }
    
    //Setter for the availabity to hit
    public void SetHit(bool hit =false)
    {
        canHit= hit;
        
    }
}