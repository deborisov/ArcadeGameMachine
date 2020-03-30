using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTowerScript : MonoBehaviour
{
    //StageCleared Tower TowerStages Stage
    public GameObject MainMenu;
    public GameObject TowerMenu;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Tower", 0) == 1)
        {
            MainMenu.SetActive(false);
            TowerMenu.SetActive(true);
        }
    }
}
