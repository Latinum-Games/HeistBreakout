using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour
{
    private MovementV2 movementV2;
    private bool isMoving =false;
    [Header("Hitboxes")]
    public GameObject upHit;
    public GameObject downHit;
    public GameObject leftHit;
    public GameObject rightHit;
    
    // Start is called before the first frame update
    void Start()
    {
        movementV2 = GetComponent<MovementV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkMove())
        {Hitboxer();}
    }
    private bool checkMove()
    {
        if(movementV2.verticalDirection ==0 && movementV2.horizontalDirection==0 )
        isMoving=false;
        else
        isMoving=true;

        return isMoving;
    }
    private void Hitboxer()
    {if(movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Up)
        {
            upHit.SetActive(true);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            //movementV2.lookState= MovementV2.PlayerLookState.Up;
            
        }
        else if(movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Down)
        {
            upHit.SetActive(false);
            downHit.SetActive(true);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            //movementV2.lookState= MovementV2.PlayerLookState.Down;
        }
        else if(movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Right)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(true);
            //movementV2.lookState= MovementV2.PlayerLookState.Right;
        }
        else if(movementV2.GetPlayerLookState()== MovementV2.PlayerLookState.Left)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(true);
            rightHit.SetActive(false);
            //movementV2.lookState= MovementV2.PlayerLookState.Left;
        }
        else{
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
        }}
}
