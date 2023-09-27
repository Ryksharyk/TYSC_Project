using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{
    Vector2 startPosition;
    public int Startlvl;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        SceneManager.LoadScene(Startlvl);
    }
}

