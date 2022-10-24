using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * script de controlador de personaje
 * Autor: Jared Abraham Flores Guarneros
*/
public class Movement : MonoBehaviour
{
    public float velocidad = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    
   // public string xvariable;
    //public string yvariable;
    public string toLoadX;
    public string toLoadY;

    void Awake()
    {
        //se aplica la posicion inicial del personaje
        float p_x = transform.position.x;
        float p_y = transform.position.y;
        p_x = PlayerPrefs.GetFloat(toLoadX);
        p_y = PlayerPrefs.GetFloat(toLoadY);
        PlayerPrefs.SetInt("Scene",SceneManager.GetActiveScene().buildIndex);
        transform.position= new Vector2(p_x,p_y);
    }

    // Update is called once per frame
    void Update()
    {
        //se actualiza la posicion del jugador con las flechas
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
