using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour
{
    //Intitializes movement controller with moving indicator
    private MovementV2 movementV2;
    private bool isMoving =false;
    private MovementV2.PlayerLookState PlayerLookState;
    //Initializes the hitboxes from all directions
    [Header("Hitboxes")]
    public GameObject upHit;
    public GameObject downHit;
    public GameObject leftHit;
    public GameObject rightHit;
    
    // Start is called before the first frame update
    void Start()
    {
        //Gets movement controller
        movementV2 = GetComponent<MovementV2>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks movement for enabling hitboxes
        if(checkMove())
        {Hitboxer();}
    }
    
    //Checks moving from vertical and horizontal directions
    private bool checkMove()
    {
        if(movementV2.verticalDirection ==0 && movementV2.horizontalDirection==0 )
        isMoving=false;
        else
        isMoving=true;

        return isMoving;
    }
    
    //Sets the active hitboxes
    private void Hitboxer() {
        PlayerLookState = movementV2.GetPlayerLookState();
        SetActiveHitBox(PlayerLookState);
    }

    //Sets the active box depending on the player look state
    private void SetActiveHitBox(MovementV2.PlayerLookState activeHitbox) {
        upHit.SetActive(false);
        downHit.SetActive(false);
        leftHit.SetActive(false);
        rightHit.SetActive(false);
        
        if (activeHitbox == MovementV2.PlayerLookState.Left) {
            leftHit.SetActive(true);
        } else if (activeHitbox == MovementV2.PlayerLookState.Down) {
            downHit.SetActive(true);
        } else if (activeHitbox == MovementV2.PlayerLookState.Right) {
            rightHit.SetActive(true);
        }else if (activeHitbox == MovementV2.PlayerLookState.Up) {
            upHit.SetActive(true);
        }
    }
}