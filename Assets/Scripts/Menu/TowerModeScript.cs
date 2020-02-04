using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerModeScript : MonoBehaviour
{
    public GameObject TowerPrefab;

    public GameObject ArcanoidIcon;
    public GameObject PepeIcon;
    public GameObject PongIcon;
    List<GameObject> tower = new List<GameObject>();
    List<GameObject> icons = new List<GameObject>();
    List<GameObject> possibleIcons = new List<GameObject>();
    List<int> stages = new List<int>();
    public TextMeshProUGUI stageText;

    enum Games{
        Arkanoid,
        FlappyFrog,
        Pong
    }

    public void Awake()
    {
        possibleIcons.Add(ArcanoidIcon);
        possibleIcons.Add(PepeIcon);
        possibleIcons.Add(PongIcon);
    }
    public void Reroll()
    {
        DisposeTower();
        float objectHeight = TowerPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        for (int i = 0; i < GetNumberOfStages(); ++i)
        {
            GameObject towerStage = Instantiate(TowerPrefab) as GameObject;
            Transform t = towerStage.transform;
            t.SetParent(transform);
            t.transform.position = new Vector3(-screenBounds.x/2, -screenBounds.y + i*objectHeight + objectHeight / 2, 0);
            tower.Add(towerStage);

            int curStage = Random.Range(0, possibleIcons.Count);
            stages.Add(curStage);
            GameObject stage;
            Transform tr;
            switch (curStage)
            {
                case 0:
                    stage = Instantiate(ArcanoidIcon) as GameObject;
                    tr = stage.transform;
                    tr.SetParent(t);
                    tr.transform.position = new Vector3(-screenBounds.x / 2, -screenBounds.y + i * objectHeight + objectHeight / 2, 0);
                    icons.Add(stage);
                    break;
                case 1:
                    stage = Instantiate(PepeIcon) as GameObject;
                    tr = stage.transform;
                    tr.SetParent(t);
                    tr.transform.position = new Vector3(-screenBounds.x / 2, -screenBounds.y + i * objectHeight + objectHeight / 2, 0);
                    icons.Add(stage);
                    break;
                case 2:
                    stage = Instantiate(PongIcon) as GameObject;
                    tr = stage.transform;
                    tr.SetParent(t);
                    tr.transform.position = new Vector3(-screenBounds.x / 2, -screenBounds.y + i * objectHeight + objectHeight / 2, 0);
                    icons.Add(stage);
                    break;
            }
        }
        stageText.text = $"Stage 1/{icons.Count}";
    }

    public void DisposeTower()
    {
        for (int i = 0; i < tower.Count; ++i)
        {
            Destroy(tower[i]);
        }
        tower = new List<GameObject>();
        icons = new List<GameObject>();
        stages = new List<int>();
    }

    int GetNumberOfStages()
    {
        switch (PlayerPrefs.GetInt("Difficuly", 1))
        {
            case 0: return 5;
            case 1: return 10;
            case 2: return 15;
            default: return 10;
        }
    }
}
