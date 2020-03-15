﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyBirdGameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    int score = 0;
    bool gameOver = true;
    bool won = false;
    public bool Won { get { return won; } private set { won = value; } }

    public bool GameOver { get { return gameOver; } }
    public static FlappyBirdGameManager Instance;
    public GameObject countdownPage;
    public GameObject gameOverPage;

    public enum ScoreToDif
    {
        Easy = 3,
        Medium = 6,
        Hard = 9
    }

    enum PageState
    {
        None,
        CountDown, 
        GameOver
    }

    public TextMeshProUGUI scoreText;

    void Start()
    {
        SetScoreText();
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
        GameObject gameOverText = gameOverPage.transform.Find("GameOverText").gameObject;
        GameObject menuButton = gameOverPage.transform.Find("MenuButton").gameObject;
        GameObject restartButton = gameOverPage.transform.Find("RestartButton").gameObject;
        if (Won)
        {
            gameOverText.GetComponent<TextMeshProUGUI>().text = "Stage Clear!";
            menuButton.SetActive(false);
            restartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next stage";
            PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage", 0)); //?
            Button b = restartButton.GetComponent<Button>();
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(PauseMenu.instance.GoToMenu);

        }
        else
        {
            PlayerPrefs.SetInt("StageCleared", 0);
            gameOverText.GetComponent<TextMeshProUGUI>().text = "You died!";
            if (PlayerPrefs.GetInt("Tower", 0) == 0)
            {
                restartButton.SetActive(true);
            }
            else
            {
                restartButton.SetActive(false);
            }
            menuButton.SetActive(true);
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        SetScoreText();
        if (PlayerPrefs.GetInt("Tower", 0) == 1 && score >= GetScoreByDifficulty())
        {
            Won = true;
            PlayerPrefs.SetInt("StageCleared", 1);
        }
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
        SetScoreText();
    }

    public void Restart()
    {
        SceneManager.LoadScene("FlappyFrog");
    }

    public int GetScoreByDifficulty()
    {
        return (PlayerPrefs.GetInt("Difficulty", 1) + 1)/**3*/;
    }

    public void SetScoreText()
    {
        if (PlayerPrefs.GetInt("Tower", 0) == 0)
        {
            scoreText.text = score.ToString();
        }
        else
        {
            scoreText.text = "0/" + GetScoreByDifficulty();
        }
    }
}
