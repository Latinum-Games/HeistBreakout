using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * script controlador del jugador en minijuego
 * Autor: Jared Abraham Flores Guarneros
*/
public class MoviMini : MonoBehaviour
{
    public float velocidad = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    void Update()
    {
        //se actualiza la posicion del juagdor con las flechas
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        if (movement.y == 0)
        {
            movement.x = Input.GetAxis("Horizontal");

        };
        
        if (movement.x == 0){
            movement.y = Input.GetAxis("Vertical");

        };
        


        rb.MovePosition(rb.position + movement * velocidad * Time.fixedDeltaTime);
        
    }
    
}
