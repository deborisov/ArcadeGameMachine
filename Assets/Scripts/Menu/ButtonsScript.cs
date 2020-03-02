using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayArcanoid()
    {
        SceneManager.LoadScene("Arcanoid");
        PlayerPrefs.SetInt("Tower", 0);
    }

    public void PlayFlappyFrog()
    {
        SceneManager.LoadScene("FlappyFrog");
        PlayerPrefs.SetInt("Tower", 0);
    }

    public void Play2dPong()
    {
        SceneManager.LoadScene("2dPong");
        PlayerPrefs.SetInt("Tower", 0);
    }
}
