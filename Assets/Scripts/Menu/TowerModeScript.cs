using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Delaunay;

public class SaveTower
{
    public List<TowerModeScript.Games> stages;
    public SaveTower(List<TowerModeScript.Games> stages)
    {
        this.stages = stages;
    }
}

public class TowerModeScript : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject PlayButton;
    public GameObject Bottom;
    //public GameObject Test;

    public GameObject ArcanoidIcon;
    public GameObject PepeIcon;
    public GameObject PongIcon;
    List<GameObject> tower = new List<GameObject>();
    List<GameObject> icons = new List<GameObject>();
    List<GameObject> possibleIcons = new List<GameObject>();
    List<Games> stages = new List<Games>();
    public TextMeshProUGUI stageText;

    public enum Games
    {
        Arkanoid,
        FlappyFrog,
        Pong
    }

    public void Awake()
    {
        SetBottom();
        possibleIcons.Add(ArcanoidIcon);
        possibleIcons.Add(PepeIcon);
        possibleIcons.Add(PongIcon);
        if (stages.Count == 0)
        {
            PlayButton.SetActive(false);
        }
        List<Games> curStages = GetCurrentStages();
        if (curStages != null)
        {
            DisplayTower(curStages);
        }
    }

    void SetBottom()
    {
        var boxCollider = Bottom.GetComponent<BoxCollider2D>();
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //Bottom.transform.position = new Vector2(0, -screenBounds.y - 1f);
        //Bottom.transform.forward = new Vector2(2 * screenBounds.x + 1.5f, 1);
        boxCollider.size = new Vector2(2 * screenBounds.x + 1.5f, 1);
        Debug.Log(screenBounds.y);
        boxCollider.offset = new Vector2(0, -screenBounds.y -0.5f);
        boxCollider.isTrigger = false;
    }

    public void Reroll()
    {
        //Sure to reroll?
        DisposeTower();
        List<Games> proStages = new List<Games>();
        for (int i = 0; i < GetNumberOfStages(); ++i)
        {
            int curStage = Random.Range(0, possibleIcons.Count);
            while (i != 0 && (int)proStages[i - 1] == curStage)
            {
                curStage = Random.Range(0, possibleIcons.Count);
            }
            proStages.Add((Games)curStage);
        }
        DisplayTower(proStages);
    }

    public void SaveTowerState()
    {
        SaveTower data = new SaveTower(stages);
        string value = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("TowerStages", value);
    }

    public void DisposeTower()
    {
        NullStage();
        for (int i = 0; i < tower.Count; ++i)
        {
            Destroy(tower[i]);
            Destroy(icons[i]);
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
        SaveTowerState();
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

    public void DisplayTower(List<Games> stages)
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
        SaveTowerState();
    }

    public GameObject BuildBrick(int i, GameObject Prefab)
    {
        float objectHeight = TowerPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject towerStage = Instantiate(Prefab) as GameObject;
        Transform t = towerStage.transform;
        t.SetParent(transform);
        t.transform.position = new Vector3(-screenBounds.x / 2, -screenBounds.y + i * objectHeight + objectHeight / 2, 0);
        if (Prefab == TowerPrefab)
        {
            towerStage.AddComponent<BoxCollider2D>();
            towerStage.GetComponent<BoxCollider2D>().isTrigger = false;
            towerStage.AddComponent<Explodable>();
            var e = towerStage.GetComponent<Explodable>();
            e.extraPoints = 20;
            e.shatterType = Explodable.ShatterType.Voronoi;
            e.allowRuntimeFragmentation = true;
            //e.explode();
            towerStage.AddComponent<ExplodeOnClick>();
        }
        return towerStage;
    }

    private List<Games> GetCurrentStages()
    {
        if (!PlayerPrefs.HasKey("TowerStages"))
        {
            return null;
        }
        string jsonData = PlayerPrefs.GetString("TowerStages");
        SaveTower st = JsonUtility.FromJson<SaveTower>(jsonData);
        if (st.stages.Count == 0)
        {
            return null;
        }
        if (PlayerPrefs.GetInt("StageCleared", 0) == 1)
        {
            st.stages.RemoveAt(0);
            PlayerPrefs.SetInt("StageCleared", 0);
            //Если длина 0 - победа
        }
        else
        {
            Debug.Log("Not cleared");
            DisposeTower();
            PlayButton.SetActive(false);
            return null;
            //Сообщение о поражении
        }
        return st.stages;
    }
}
