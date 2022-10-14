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
    private MovementV2 movementV2;

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
        hitting();
        
        
        }
    }
    public void hitting()
    {
        if (movementV2.up && canHit) 
            {
                
                StartCoroutine(upHit());
                
             
                
            }
            if (movementV2.down && canHit)
            {
                
                StartCoroutine(downHit());
               
            }
            if (movementV2.left && canHit)
            {
                
                StartCoroutine(leftHit());
             
            }
            if (movementV2.right && canHit)
            {
                
                StartCoroutine(rightHit());
                
            }
                
                //new WaitForSeconds(5);

    }
    public void SetHit(bool hit =false)
    {
        canHit= hit;
        Debug.Log(canHit);


}
IEnumerator leftHit()
{
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(true);
    rightbox.SetActive(false);
    yield return new WaitForSeconds(2);
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    canHit=false;
    

}
IEnumerator rightHit()
{
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(true);
    yield return new WaitForSeconds(2);
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    canHit=false;
    

}
IEnumerator upHit()
{
    upbox.SetActive(true);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    yield return new WaitForSeconds(2);
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    canHit=false;
    

}
IEnumerator downHit()
{
    upbox.SetActive(false);
    downbox.SetActive(true);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    yield return new WaitForSeconds(2);
    upbox.SetActive(false);
    downbox.SetActive(false);
    leftbox.SetActive(false);
    rightbox.SetActive(false);
    canHit=false;
    

}
}
