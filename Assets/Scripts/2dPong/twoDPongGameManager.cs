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
    public bool gameStarted = false;

    public List<GameObject> upgrades;
    enum PageState
    {
        None,
        Start, 
        GameOver
    }

    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (PlayerPrefs.GetInt("Tower", 0) == 1){
            scoreText.text = "Score 0/" + GetScoreByDifficulty();
        }
        else
        {
            scoreText.text = "Score 0";
        }
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
        Ball2DScript.OnPlayerScored += ClearMap;
        Ball2DScript.OnPlayerDied += OnPlayerDied;
        OnGameStarted += ZeroizeTime;
    }

    private void OnDisable()
    {
        StartTap.OnTapHappened -= OnTapHappened;
        Ball2DScript.OnPlayerScored -= IncrementScoreAndSetStart;
        Ball2DScript.OnPlayerScored -= ClearMap;
        Ball2DScript.OnPlayerDied -= OnPlayerDied;
        OnGameStarted -= ZeroizeTime;
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (Random.Range(0, 300) < 1)
            {
                int chanceForUpgrade = Random.Range(1, 3);
                switch (chanceForUpgrade)
                {
                    case 1: Instantiate(upgrades[0], GetRandomPosition(), Quaternion.Euler(0, 0, 45)); break;
                    case 2: Instantiate(upgrades[1], GetRandomPosition(), transform.rotation); break;
                }
            }
            currentTime += Time.deltaTime;
            if (currentTime > secondsToHastenGame)
            {
                Time.timeScale += addedHaste;
                currentTime -= secondsToHastenGame;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float x = Random.Range(-screenBounds.x, screenBounds.x) * 0.8f;
        float y = Random.Range(-screenBounds.y, screenBounds.y) * 0.8f;
        return new Vector3(x, y, 0);
    }

    private void IncrementScoreAndSetStart()
    {
        ++score;
        if (PlayerPrefs.GetInt("Tower", 0) == 1)
        {
            scoreText.text = "Score " + score + "/" + GetScoreByDifficulty();
        }
        else
        {
            scoreText.text = "Score " + score;
        }
        if (PlayerPrefs.GetInt("Tower", 0) == 1 && score == GetScoreByDifficulty())
        {
            won = true;
            if (PlayerPrefs.GetInt("Tower", 0) == 1)
            {
                PlayerPrefs.SetInt("StageCleared", 1);
            }
            OnPlayerDied();
        }
        else
        {
            SetPageState(PageState.Start);
        }
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
        gameStarted = true;
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
        ClearMap();
        gameStarted = false;
        SetPageState(PageState.Start);
        scoreText.text = "Score 0";
        //SceneManager.LoadScene("2dPong");
    }

    private void ClearMap()
    {
        var doubles = GameObject.FindGameObjectsWithTag("Double");
        foreach (var d in doubles)
        {
            Destroy(d);
        }
        var speeds = GameObject.FindGameObjectsWithTag("Speed");
        foreach (var s in speeds)
        {
            Destroy(s);
        }
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
