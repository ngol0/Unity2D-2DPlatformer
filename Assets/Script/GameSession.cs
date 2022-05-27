using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    //ref
    [SerializeField] TextMeshProUGUI myLives;
    [SerializeField] TextMeshProUGUI myScore;

    private static GameSession _instance;

    public static GameSession Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
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
        ResetGameData();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ResetGameData()
    {
        ScenePersist.Instance.ResetScenePersist();
        Destroy(gameObject);
    }
}
