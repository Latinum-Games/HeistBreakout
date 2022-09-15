using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseHear : MonoBehaviour
{

 
    void Awake(){
     
     
    }
 
    // Use this for initialization
    void Start () {
     
      
     
    }
 
         
         
    
     
 
 
    // Update is called once per frame
    void Update () {
 
 
        }
    void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sound"))
        {
            Debug.Log("Te escuche");
        }

 
        
 
            
        
 
            }
            
    }
    
 
 

