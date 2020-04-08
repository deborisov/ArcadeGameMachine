using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Toggle joystickStateToggle;
    public TMP_Dropdown difficulty;
    public GameObject towerMode;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
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
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        
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

    public void SetVolume(float value)
    {
        //audioMixer.SetFloat("Volume", value);
        PlayerPrefs.SetFloat("Volume", value);
    }
}
