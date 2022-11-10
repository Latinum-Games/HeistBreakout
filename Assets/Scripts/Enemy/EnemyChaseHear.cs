using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseHear : MonoBehaviour
{
    [Header("Movement script AI")]
    private EnemyFieldOfView scriptFOV;

    private void Start() {
        scriptFOV = GetComponent<EnemyFieldOfView>();
    }

    void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sound")) {
            scriptFOV.State = EnemyFieldOfView.EnemyState.Persecution;
            Debug.Log("Te escuche");
        }
    }

}