using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle joystickStateToggle;
    public void Awake()
    {
        joystickStateToggle.isOn = PlayerPrefs.GetInt("Joystick") == 1? true: false;
    }
    public void ChangeJoystickPrefs(bool newValue)
    {
        if (!newValue)
        {
            PlayerPrefs.SetInt("Joystick", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Joystick", 1);
        }
        Debug.Log(PlayerPrefs.GetInt("Joystick"));
    }
}
