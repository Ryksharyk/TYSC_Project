using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("shoot", 2);        
        }

    }
    public GameObject bullet;
    public Transform bulletPos;
    public void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
