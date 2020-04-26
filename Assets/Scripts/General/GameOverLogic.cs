    using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverLogic
{
    public GameObject gameOverPage;

    public GameOverLogic(GameObject gameOverPage)
    {
        this.gameOverPage = gameOverPage;
    }

    public void DrawPage(bool won)
    {
        GameObject gameOverText = gameOverPage.transform.Find("GameOverText").gameObject;
        GameObject menuButton = gameOverPage.transform.Find("MenuButton").gameObject;
        GameObject restartButton = gameOverPage.transform.Find("RestartButton").gameObject;
        if (won)
        {
            if (PlayerPrefs.GetInt("Tower", 0) == 1)
            {
                gameOverText.GetComponent<TextMeshProUGUI>().text = "Stage cleared!";
                menuButton.SetActive(false);
                restartButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next stage";
                //PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage", 0)); //?
                Button b = restartButton.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(PauseMenu.instance.GoToMenu);
            }
            else
            {
                gameOverText.GetComponent<TextMeshProUGUI>().text = "You won!";
            }
        }
        else
        {
            PlayerPrefs.SetInt("StageCleared", 0);
            gameOverText.GetComponent<TextMeshProUGUI>().text = "You died!";
            if (PlayerPrefs.GetInt("Tower", 0) == 0)
            {
                restartButton.SetActive(true);
            }
            else
            {
                restartButton.SetActive(false);
            }
            menuButton.SetActive(true);
        }
    }
}
