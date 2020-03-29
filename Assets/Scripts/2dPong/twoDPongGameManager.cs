using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class twoDPongGameManager : MonoBehaviour
{
    public GameObject StartPage;
    public GameObject GameOverPage;
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static twoDPongGameManager Instance;
    int score = 0;
    bool gameOver = true;
    bool won = false;
    public BoxCollider2D leftEdge, rightEdge, topEdge, bottomEdge;
    private float currentTime = 0;
    public float secondsToHastenGame = 1, addedHaste = 1f / 60;
    public bool GameOver { get { return gameOver; } }
    private GameOverLogic gameOverLogic;
    enum PageState
    {
        None,
        Start, 
        GameOver
    }

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score 0/" + GetScoreByDifficulty();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        leftEdge.size = rightEdge.size = new Vector2(1, 2 * screenBounds.y + 1.5f);
        topEdge.size = bottomEdge.size = new Vector2(2 * screenBounds.x + 1.5f, 1);
        topEdge.transform.position = new Vector2(0, screenBounds.y + 0.5f);
        bottomEdge.transform.position = new Vector2(0, -screenBounds.y - 0.5f);
        leftEdge.transform.position = new Vector2(-screenBounds.x - 0.5f, 0);
        rightEdge.transform.position = new Vector2(screenBounds.x + 0.5f, 0);
        SetPageState(PageState.Start);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        StartTap.OnTapHappened += OnTapHappened;
        Ball2DScript.OnPlayerScored += IncrementScoreAndSetStart;
        Ball2DScript.OnPlayerDied += OnPlayerDied;
        OnGameStarted += ZeroizeTime;
    }

    private void OnDisable()
    {
        StartTap.OnTapHappened -= OnTapHappened;
        Ball2DScript.OnPlayerScored -= IncrementScoreAndSetStart;
        Ball2DScript.OnPlayerDied -= OnPlayerDied;
        OnGameStarted -= ZeroizeTime;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > secondsToHastenGame)
        {
            Time.timeScale += addedHaste;
            currentTime -= secondsToHastenGame;
        }
    }

    private void IncrementScoreAndSetStart()
    {
        ++score;
        scoreText.text = "Score " + score+"/"+GetScoreByDifficulty(); 
        if (score == GetScoreByDifficulty())
        {
            won = true;
            if (PlayerPrefs.GetInt("Tower", 0 ) == 1)
            {
                PlayerPrefs.SetInt("StageCleared", 1);
            }
            OnPlayerDied();
        }
        SetPageState(PageState.Start);
    }

    private void OnPlayerDied()
    {
        gameOver = true;
        Joystick.IsAwake = false;
        SetPageState(PageState.GameOver);
        gameOverLogic = new GameOverLogic(GameOverPage);
        gameOverLogic.DrawPage(won);
    }

    void OnTapHappened()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        Joystick.IsAwake = true;
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                StartPage.SetActive(false);
                GameOverPage.SetActive(false);
                break;
            case PageState.Start:
                StartPage.SetActive(true);
                GameOverPage.SetActive(false);
                break;
            case PageState.GameOver:
                StartPage.SetActive(false);
                GameOverPage.SetActive(true);
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("2dPong");
    }

    private void ZeroizeTime()
    {
        currentTime = 0;
        Time.timeScale = 1;
    }

    public int GetScoreByDifficulty()
    {
        return (PlayerPrefs.GetInt("Difficulty", 1) + 1);
    }
}
