using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlappyBirdGameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;
    int score = 0;
    bool gameOver = false;
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
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        SetPageState(PageState.CountDown);
    }

    private void Awake()
    {
        Instance = this;
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
}
