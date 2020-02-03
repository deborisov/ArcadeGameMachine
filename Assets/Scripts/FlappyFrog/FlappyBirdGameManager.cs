using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlappyBirdGameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    int score = 0;
    bool gameOver = true;

    public bool GameOver { get { return gameOver; } }
    public static FlappyBirdGameManager Instance;
    public GameObject countdownPage;
    public GameObject gameOverPage;

    enum PageState
    {
        None,
        CountDown, 
        GameOver
    }

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "0";
        SetPageState(PageState.CountDown);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CountDown.OnTapHappened += OnTapHappened;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    private void OnDisable()
    {
        CountDown.OnTapHappened -= OnTapHappened;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnTapHappened()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
        Time.timeScale = 1f;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                countdownPage.SetActive(false);
                gameOverPage.SetActive(false);
                break;
            case PageState.CountDown:
                countdownPage.SetActive(true);
                gameOverPage.SetActive(false);
                break;
            case PageState.GameOver:
                countdownPage.SetActive(false);
                gameOverPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        OnGameOverConfirmed();
        scoreText.text = "0";
    }

    public void Restart()
    {
        SceneManager.LoadScene("FlappyFrog");
    }
}
