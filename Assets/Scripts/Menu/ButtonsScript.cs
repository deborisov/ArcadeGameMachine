using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayArcanoid()
    {
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("Arcanoid");
    }

    public void PlayFlappyFrog()
    {
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("FlappyFrog");
    }

    public void Play2dPong()
    {
        PlayerPrefs.SetInt("Tower", 0);
        SceneManager.LoadScene("2dPong");
    }
}
