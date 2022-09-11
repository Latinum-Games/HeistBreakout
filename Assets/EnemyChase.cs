
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform cast;
    public Transform player;
    public float agroRange;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public bool facingLeft;
    private bool isAgro =false;
    private bool isSearching =false;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //dir = new Vector3(player.position.x, player.position.y, 0);
        if(CanSeePlayer(agroRange))
        {
            isAgro = true;
            
        }
        else
        {
            if(isAgro)
            {
                //isSearching = true;
                if(!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 3);
                }
                
            }
            
        }
        if(isAgro)
        {
            ChasePlayer();
        }
        
    }
    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            //rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            facingLeft = false;

        }
        else
        {
            //rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            facingLeft = true;

        }
        direction = player.position - transform.position;
        rb2d.AddRelativeForce(direction.normalized * moveSpeed, ForceMode2D.Force);

    }
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
        isAgro = false;
        isSearching=false;

    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = distance;

        if(facingLeft ==true)
        {
            castDist = -distance;
        }

        Vector2 endPos = cast.position + Vector3.right * castDist;
        //Vector2 endPos = cast;
        RaycastHit2D hit = Physics2D.Linecast(cast.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        if(hit.collider !=null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(cast.position, hit.point, Color.green);

        }
        else
        {
            Debug.DrawLine(cast.position, endPos, Color.blue);
        }
        return val;


    }
}
