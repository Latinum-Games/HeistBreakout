using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controlador de IA de un enemigo
 * Autor: Jared Abraham Flores Guarneros
*/
public class Enemy : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public bool shouldRotate;
    public LayerMask whatIsPlayer;
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;
    private bool isInChaseRange;
    private bool isInAttackRange;
    private void Start()
    {
        //se obtienen los componentes del enemigo
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
        //se cambia la animacion cuando se empieza a mover
        anim.SetBool("running",isInChaseRange);
        //se crea un circulo alrededor del enemigo para detectar al jugador
        isInChaseRange = Physics2D.OverlapCircle(transform.position,checkRadius,whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position,attackRadius,whatIsPlayer);
        //se actualiza la posicion del enemigo con respecto al jugador
        dir = target.position- transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("x",dir.x);
            anim.SetFloat("y",dir.y);

        }

    }
    private void FixedUpdate()
    {
        //sehace un chequeo de si el enemigo puede dejar de atacar o sigue acercandose
        if(isInChaseRange&&!isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void MoveCharacter(Vector2 dir)
    {
        //se actualiza la posicion del enemigo
        rb.MovePosition((Vector2)transform.position+ (dir * speed * Time.deltaTime ));
    }

}
