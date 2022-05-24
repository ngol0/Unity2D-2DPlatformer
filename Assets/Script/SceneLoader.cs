using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    int startingSceneIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = currentSceneIndex+1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startingSceneIndex);
    }
}
