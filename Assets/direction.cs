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
    {if(movementV2.up)
        {
            upHit.SetActive(true);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            movementV2.up=true;
            movementV2.down=false;
            movementV2.left=false;
            movementV2.right=false;
            
        }
        else if(movementV2.down)
        {
            upHit.SetActive(false);
            downHit.SetActive(true);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
            movementV2.up=false;
            movementV2.down=true;
            movementV2.left=false;
            movementV2.right=false;
        }
        else if(movementV2.right)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(true);
            movementV2.up=false;
            movementV2.down=false;
            movementV2.left=false;
            movementV2.right=true;
        }
        else if(movementV2.left)
        {
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(true);
            rightHit.SetActive(false);
            movementV2.up=false;
            movementV2.down=false;
            movementV2.left=true;
            movementV2.right=false;
        }
        else{
            upHit.SetActive(false);
            downHit.SetActive(false);
            leftHit.SetActive(false);
            rightHit.SetActive(false);
        }}
}
