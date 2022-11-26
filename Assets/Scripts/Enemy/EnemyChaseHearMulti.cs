using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseHearMulti : MonoBehaviour
{
    [Header("Movement script AI")]
    private EnemyFieldOfViewMulti scriptFOV;

    private void Start() {
        scriptFOV = GetComponent<EnemyFieldOfViewMulti>();
    }

    void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sound")) {
            scriptFOV.State = EnemyFieldOfViewMulti.EnemyState.Persecution;
            Debug.Log("Te escuche");
        }
    }

}