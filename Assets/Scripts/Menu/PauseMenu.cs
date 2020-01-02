using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsOnPause = false;
    public GameObject pauseUI;
    public GameObject pauseBtn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("LOL");
            if (!GameIsOnPause)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOnPause = false;
        if (pauseBtn != null)
        {
            pauseBtn.SetActive(true);
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOnPause = true;
        if (pauseBtn != null)
        {
            pauseBtn.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
