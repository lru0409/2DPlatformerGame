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
    public GameObject stageMap;
    public GameObject gameUI;

    public void DisplayEventUI(string title)
    {
        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();

        UIEventTitle.text = title;
        if (title == "Stage Clear!") {
            playButtonText.text = "다음 스테이지로";
        } else { // Fail to Clear
            playButtonText.text = "다시 시도하기";
        }
        StageEventUI.SetActive(true);
    }

    public void GoToPlayStage()
    {
        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();

        StageEventUI.SetActive(false);
        if (playButtonText.text == "다음 스테이지로") {
            gameManager.stages[gameManager.stageIndex].SetActive(false);
            gameManager.stageIndex++;
            gameManager.stageIndex++;
            gameManager.stageOpened[gameManager.stageIndex] = true;
        } else if (playButtonText.text == "다시 시도하기") {
            gameManager.stages[gameManager.stageIndex].SetActive(false);
        }
        gameManager.GoToStage(gameManager.stageIndex + 1);
    }

    public void GoToStageMap()
    {
        gameManager.stages[gameManager.stageIndex].SetActive(false);
        StageEventUI.SetActive(false);
        gameUI.SetActive(false);

        gameManager.stageIndex++;
        gameManager.stageOpened[gameManager.stageIndex] = true;
        stageMap.SetActive(true);
    }

}
