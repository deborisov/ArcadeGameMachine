using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Toggle joystickStateToggle;
    public TMP_Dropdown difficulty;
    public GameObject towerMode;
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public void Awake()
    {
        joystickStateToggle.isOn = PlayerPrefs.GetInt("Joystick", 1) == 1? true: false;
        difficulty.value = PlayerPrefs.GetInt("Difficulty", (int)Difficulty.Medium);
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
    }

    public void ChangeDifficulty(int newValue)
    {
        towerMode.GetComponent<TowerModeScript>().DisposeTower();
        PlayerPrefs.SetInt("Difficulty", newValue);
        PlayerPrefs.DeleteKey("TowerStages");
    }
}
