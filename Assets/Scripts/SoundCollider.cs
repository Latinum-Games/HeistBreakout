
using UnityEngine;
using UnityEngine.UI;

public class SoundCollider : MonoBehaviour
{
    CircleCollider2D p_Collider ;
    float m_ScaleX;
    public GameObject player;
    private Player playerScript;
    public float rad =1.0f;
    public GameObject renderer;
    private float mult;



    // Start is called before the first frame update
    void Start()
    {


        p_Collider = GetComponent<CircleCollider2D>();
        playerScript = player.GetComponent<Player>();



    }

    // Update is called once per frame
    void Update()
    {
        mult = playerScript.maxSoundArea;
        //p_Collider.radius = ((playerScript.mag)/2)*(mult/2);
        //rad = p_Collider.radius;
        renderer.transform.localScale = new Vector3(playerScript.mag*mult, playerScript.mag * mult, playerScript.mag * mult);

        //Debug.Log(playerScript.mag);
        //Debug.Log("Current CircleCollider Size : " + p_Collider.radius);

    }
    void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(this.transform.position, rad);
    }

}
