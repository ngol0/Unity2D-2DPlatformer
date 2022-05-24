using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] int playerLives = 3;
    [SerializeField] int score {get;set;} = 0;

    //ref
    [SerializeField] TextMeshProUGUI myLives;
    [SerializeField] TextMeshProUGUI myScore;

    //Singleton
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        myLives.text = playerLives.ToString();
        myScore.text = score.ToString();
    }


    public void AddScore (int scoreValue)
    {
        score += scoreValue;
        myScore.text = score.ToString();
    }

    public void processPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(currentSceneIndex);
        myLives.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(currentSceneIndex);
        Destroy(gameObject);
    }

}
