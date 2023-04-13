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

    public Image[] UIHp;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartButton;

    public bool[] stageOpened = new bool[12]{true, false, false, false, false, false, false, false, false, false, false, false};

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void ClearStage()
    {
        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;

        Time.timeScale = 0;
        Stages[stageIndex].SetActive(false);
        if (stageIndex < Stages.Length - 1)
        {
            stageIndex++;
            stageOpened[stageIndex] = true;
            // 스테이지 클리어 화면 활성화 - 스테이지 맵으로, 다음 스테이지로
        } else {
            Debug.Log("Clear All Stage");
        }
        StageMap.SetActive(true); // 임시
    }

    public void GoToStage(int stageNumber)
    {
        Stages[stageNumber - 1].SetActive(true);
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
            UIRestartButton.SetActive(true);
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

    public void RestartClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
