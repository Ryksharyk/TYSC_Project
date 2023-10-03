using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform : MonoBehaviour
{
    private GameObject CrackedWall;
    [SerializeField] Collider2D Hydraulicscollider;
    [SerializeField] Rigidbody2D rb;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        CrackedWall = GameObject.FindGameObjectWithTag("Cracked Wall");

        rb.velocity = new Vector2(10,rb.velocity.y) * Force;
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cracked Wall"))
        {
            Destroy(gameObject);
            Destroy(CrackedWall);    
        }
        

    }
}
