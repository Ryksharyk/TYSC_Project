//Kill Player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayer : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Respawn();
        }
        if (collision.CompareTag("Hydraulic"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}

