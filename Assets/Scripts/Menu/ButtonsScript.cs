using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    public void PlayArcanoid()
    {
        SceneManager.LoadScene("Arcanoid");
    }

    public void PlayFlappyFrog()
    {
        SceneManager.LoadScene("FlappyFrog");
    }

    public void Play2dPong()
    {
        SceneManager.LoadScene("2dPong");
    }
}
