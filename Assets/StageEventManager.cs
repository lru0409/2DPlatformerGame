using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageEventManager : MonoBehaviour
{
    public GameObject StageEventUI;
    public Text UIEventTitle;
    public Button UIEventPlayButton;
    public GameManager gameManager;
    public StageMapManager stageMapManager;
    public GameObject stageMap;
    public GameObject gameUI;

    public void DisplayEventUI(string title)
    {
        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();

        UIEventTitle.text = title;
        if (title == "Stage Clear!") {
            playButtonText.text = "다음 스테이지로";
        } else { // title = "Fail to Clear"
            playButtonText.text = "다시 시도하기";
        }
        StageEventUI.SetActive(true);
    }

    public void GoToPlayStage()
    {
        StageEventUI.SetActive(false);
        gameManager.stages[gameManager.stageIndex].SetActive(false);

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로") {
            gameManager.UpdateDataToNextStage();
        } else if (playButtonText.text == "다시 시도하기") {
            // 코인이랑 몬스터 돌려놔야 하는데..
        }
        gameManager.GoToStage(gameManager.stageIndex + 1);
    }

    public void GoToStageMap()
    {
        StageEventUI.SetActive(false);
        gameManager.stages[gameManager.stageIndex].SetActive(false);
        gameUI.SetActive(false);

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로") {
            gameManager.UpdateDataToNextStage();
        }
        stageMapManager.DisplayStageMap();
    }

}
