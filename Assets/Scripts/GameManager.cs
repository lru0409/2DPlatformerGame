using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int hp = 3;
    public Player player;
    public GameObject[] Stages;
    public GameObject StageMap;
    public GameObject GameUI;
    public GameObject StageEventUI; // 클리어, 클리어 실패, 일시정지

    // GameUI
    public Image[] UIHp;
    public Text UIPoint;
    public Text UIStage;

    // StageEventUI

    public bool[] stageOpened = new bool[12]{true, false, false, false, false, false, false, false, false, false, false, false};

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    // ----- Clear Event -----

    public void ClearStage()
    {
        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;

        Time.timeScale = 0;
        if (stageIndex < Stages.Length - 1) {
            StageEventUI.SetActive(true);
        } else {
            Debug.Log("Clear All Stage");
        }
    }

    public void GoToNextStage()
    {
        Stages[stageIndex].SetActive(false);
        StageEventUI.SetActive(false);
        GameUI.SetActive(false);

        stageIndex++;
        stageOpened[stageIndex] = true;
        GoToStage(stageIndex + 1);
    }

    public void GoToStageMap()
    {
        Stages[stageIndex].SetActive(false);
        StageEventUI.SetActive(false);
        GameUI.SetActive(false);

        stageIndex++;
        stageOpened[stageIndex] = true;
        StageMap.SetActive(true);
    }

    // ----------

    public void GoToStage(int stageNumber)
    {
        Stages[stageNumber - 1].SetActive(true);
        GameUI.SetActive(true);
        player.Reposition();
        Time.timeScale = 1;
    }

    public void HpDown()
    {
        if (hp > 0) {
            hp--;
            UIHp[hp].color = new Color(1, 1, 1, 0.4f);
        }
        if (hp == 0) {
            player.OnDie();
            Debug.Log("Player Die");
            //UIRestartButton.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            HpDown();
            if (hp > 0)
                player.Reposition();
        }
    }

    // ----- Button Click Event -----

    // public void RestartClicked()
    // {
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(0);
    // }
}
