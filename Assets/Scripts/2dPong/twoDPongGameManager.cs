using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class twoDPongGameManager : MonoBehaviour
{
    public GameObject StartPage;
    public GameObject GameOverPage;
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public twoDPongGameManager Instance;
    int score = 0;
    bool gameOver = true;
    public BoxCollider2D leftEdge, rightEdge, topEdge, bottomEdge;
    public bool GameOver { get { return gameOver; } }

    enum PageState
    {
        None,
        Start, 
        GameOver
    }

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score: 0";
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
    }

    private void OnDisable()
    {
        StartTap.OnTapHappened -= OnTapHappened;
    }

    void OnTapHappened()
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
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

    void Update()
    {
        
    }
}
