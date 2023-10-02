// Script for switching level

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSwitch : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }

}
