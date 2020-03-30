using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI scoreText;
    public bool gameOver = false;
    public GameObject gameOverPannel;
    public int numberOfBricks;
    public BoxCollider2D leftEdge, rightEdge, topEdge, bottomEdge;
    private bool won = false;
    private GameOverLogic gameOverLogic;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        leftEdge.size = rightEdge.size = new Vector2(1, 2 * screenBounds.y + 1.5f);
        topEdge.size = bottomEdge.size = new Vector2(2 * screenBounds.x + 1.5f, 1);
        topEdge.offset = new Vector2(0, screenBounds.y + 0.5f);
        bottomEdge.offset = new Vector2(0, -screenBounds.y - 1f);
        leftEdge.offset = new Vector2(-screenBounds.x - 0.5f, 0);
        rightEdge.offset = new Vector2(screenBounds.x + 0.5f, 0);
        liveText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        gameOverPannel.SetActive(false);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void ChangeLives(int delta)
    {
        lives += delta;
        if (lives <= 0)
        {
            lives = 0;
            won = false;
            GameOver();
        }
        liveText.text = "Lives: " + lives;
    }

    public void ChangeNumberOfBricks()
    {
        --numberOfBricks;
        if (numberOfBricks <= 0)
        {
            won = true;
            PlayerPrefs.SetInt("StageCleared", 1);
            GameOver();
        }
    }

    public void ChangeScore(int delta)
    {
        score += delta;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0; 
        gameOver = true;
        gameOverPannel.SetActive(true);
        gameOverLogic = new GameOverLogic(gameOverPannel);
        gameOverLogic.DrawPage(won);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Arcanoid");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
