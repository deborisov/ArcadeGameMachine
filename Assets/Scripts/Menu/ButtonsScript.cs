using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayArcanoid()
    {
        PlayerPrefs.SetInt("WasPrevious", 0);
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("Arcanoid");
    }

    public void PlayFlappyFrog()
    {
        PlayerPrefs.SetInt("WasPrevious", 0);
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("FlappyFrog");
    }

    public void Play2dPong()
    {
        PlayerPrefs.SetInt("WasPrevious", 0);
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("2dPong");
    }
}
