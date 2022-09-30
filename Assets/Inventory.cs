using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxWeight=50;
    public int currentWeight=0;
    public int loot=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Collectable") )
    {
        int temp = currentWeight + col.gameObject.GetComponent<collectable>().weight;
        Debug.Log(temp);

        if(currentWeight<temp)
        {
            currentWeight+=col.gameObject.GetComponent<collectable>().weight;
            loot +=col.gameObject.GetComponent<collectable>().money;
            Destroy(col.gameObject);
        }
        
    }
    }
}
