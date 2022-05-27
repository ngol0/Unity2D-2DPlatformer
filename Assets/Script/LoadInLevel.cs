using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInLevel : MonoBehaviour
{
    //int currentScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            GameSession.Instance.ResetGameData();
            SceneManager.LoadScene("GameOver");
        }
    }
}
