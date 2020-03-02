using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SaveTower
{
    public List<TowerModeScript.Games> stages;
}

public class TowerModeScript : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject PlayButton;

    public GameObject ArcanoidIcon;
    public GameObject PepeIcon;
    public GameObject PongIcon;
    List<GameObject> tower = new List<GameObject>();
    List<GameObject> icons = new List<GameObject>();
    List<GameObject> possibleIcons = new List<GameObject>();
    List<Games> stages = new List<Games>();
    public TextMeshProUGUI stageText;

    public enum Games{
        Arkanoid,
        FlappyFrog,
        Pong
    }

    public void Awake()
    {
        possibleIcons.Add(ArcanoidIcon);
        possibleIcons.Add(PepeIcon);
        possibleIcons.Add(PongIcon);
        if (stages.Count == 0)
        {
            PlayButton.SetActive(false);
        }
        if (GetCurrentStages() != null)
        {
            DisplayTower(GetCurrentStages());
        }
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
            stages.Add((Games)curStage);
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
        SaveTowerState();
        PlayButton.SetActive(true);
    }

    public void SaveTowerState()
    {
        SaveTower data = new SaveTower();
        data.stages = stages;
        string value = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("TowerStages", value);
    }

    public void DisposeTower()
    {
        NullStage();
        for (int i = 0; i < tower.Count; ++i)
        {
            Destroy(tower[i]);
        }
        tower = new List<GameObject>();
        icons = new List<GameObject>();
        stages = new List<Games>();
    }

    int GetNumberOfStages()
    {
        switch (PlayerPrefs.GetInt("Difficulty", 1))
        {
            case 0: return 5;
            case 1: return 10;
            case 2: return 15;
            default: return 10;
        }
    }

    public void Play()
    {
        if (stages.Count == 0) return;
        PlayerPrefs.SetInt("Tower", 1);
        if (stages[0] == Games.Arkanoid)
        {
            SceneManager.LoadScene("Arcanoid");
        }
        if (stages[0] == Games.FlappyFrog)
        {
            SceneManager.LoadScene("FlappyFrog");
        }
        if (stages[0] == Games.Pong)
        {
            SceneManager.LoadScene("2dPong");
        }
    }

    public void NullStage()
    {
        PlayerPrefs.SetInt("Stage", 0);
    }

    public void DisplayTower(List <Games> stages)
    {
        for (int i = 0; i < stages.Count; ++i)
        {
            tower.Add(BuildBrick(i, TowerPrefab));
            switch (stages[i])
            {
                case Games.Arkanoid:
                    icons.Add(BuildBrick(i, ArcanoidIcon)); break;
                case Games.FlappyFrog:
                    icons.Add(BuildBrick(i, PepeIcon)); break;
                case Games.Pong:
                    icons.Add(BuildBrick(i, PongIcon)); break;
            }
        }
        this.stages = stages;
        stageText.text = $"Stage 1/{icons.Count}";
        PlayButton.SetActive(true);
    }

    public GameObject BuildBrick(int i, GameObject Prefab)
    {
        float objectHeight = TowerPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject towerStage = Instantiate(Prefab) as GameObject;
        Transform t = towerStage.transform;
        t.SetParent(transform);
        t.transform.position = new Vector3(-screenBounds.x / 2, -screenBounds.y + i * objectHeight + objectHeight / 2, 0);
        return towerStage;
    }

    private List<Games> GetCurrentStages()
    {
        if (!PlayerPrefs.HasKey("TowerStages"))
        {
            return null;
        }
        string jsonData = PlayerPrefs.GetString("TowerStages");
        Debug.Log(jsonData);
        SaveTower st = JsonUtility.FromJson<SaveTower>(jsonData);
        return st.stages;
    }
}
