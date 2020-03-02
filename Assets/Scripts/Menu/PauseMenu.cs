﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsOnPause = false;
    public GameObject pauseUI;
    public GameObject pauseBtn;
    public delegate void Paused();
    public static event Paused OnPause;
    public static PauseMenu instance;

    private void Start()
    {
        GameIsOnPause = false;
    }

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        Joystick.IsAwake = true;
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
        OnPause?.Invoke();
        Joystick.IsAwake = false;
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
