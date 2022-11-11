using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    //Animator movement values
    //1: Sneaking
    //2: Walking
    //3: Running
    
    //Initializes components of view, rigidbody and animator
    [Header("Components")]
    public Animator animator;
    private EnemyFieldOfView scriptFOV;

    private void Start() {
        scriptFOV = GetComponent<EnemyFieldOfView>();
        animator.SetInteger("Movement", 2);
    }

    private void Update() {
        animator.SetFloat("Speed", Mathf.Abs(scriptFOV.enemyVel[0]) + Mathf.Abs(scriptFOV.enemyVel[1]));
    }
}
