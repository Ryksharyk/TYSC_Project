using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    KillPlayer killplayer;

    private GameObject player;
    [SerializeField] Rigidbody2D rb;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * Force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (other.gameObject.CompareTag("Obstacle"))
            {
                killplayer.Respawn();
            }
            
            
        }
        

    }
}
