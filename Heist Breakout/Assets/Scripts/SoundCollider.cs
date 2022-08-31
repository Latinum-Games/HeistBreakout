
using UnityEngine;
using UnityEngine.UI;

public class SoundCollider : MonoBehaviour
{
    CircleCollider2D p_Collider;
    float m_ScaleX;
    public GameObject player;
    private Player playerScript;


    // Start is called before the first frame update
    void Start()
    {


        p_Collider = GetComponent<CircleCollider2D>();
        playerScript = player.GetComponent<Player>();



    }

    // Update is called once per frame
    void Update()
    {
   
        p_Collider.radius = playerScript.mag;
        Debug.Log(playerScript.mag);
        Debug.Log("Current CircleCollider Size : " + p_Collider.radius);

    }
    
}
