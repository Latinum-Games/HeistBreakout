using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExtractionPoint : MonoBehaviour {
    
    // Custom event for on trigger enter
    public UnityEvent onExtraction;

    //Avtivates when entering to the objective zone 
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            onExtraction.Invoke();
        }
    }
}
