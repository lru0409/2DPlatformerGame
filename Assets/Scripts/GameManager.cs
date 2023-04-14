using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int stagePoint;
    public int stageIndex;
    public int hp = 3;
    public Player player;
    public GameObject[] stages;
    public GameObject stageMap;
    public GameObject gameUI;
    public StageEventManager stageEventManager;

    // GameUI
    public Image[] UIHp;
    public Text UIPoint;
    public Text UIStage;

    public bool[] stageOpened = new bool[12]{true, false, false, false, false, false, false, false, false, false, false, false};

    void Update()
    {
        UIPoint.text = stagePoint.ToString();
    }

    // ----- Stage Event -----

    public void ClearStage()
    {
        Time.timeScale = 0;
        if (stageIndex < stages.Length - 1) {
            stageEventManager.DisplayEventUI("Stage Clear!");
        } else {
            Debug.Log("Clear All Stage");
        }
    }

    public void HpDown()
    {
        if (hp > 0) {
            setHP(hp - 1);
        }
        if (hp == 0) {
            player.OnDie();
            stageEventManager.DisplayEventUI("Fail to Clear");
        }
    }

    // ----------

    public void GoToStage(int stageNumber)
    {
        stages[stageNumber - 1].SetActive(true);
        gameUI.SetActive(true);

        // player setting
        player.SetActive(true);
        player.OffDie();
        player.transform.position = new Vector3(0, -0.5450001f, 0);

        stagePoint = 0;
        setHP(3);
        Time.timeScale = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            HpDown();
            if (hp > 0) {
                player.spriteRenderer.flipX = false;
                player.transform.position = new Vector3(0, 0, 0);
            }
        }
    }

    void setHP(int newHP)
    {
        hp = newHP;

        UIHp[0].color = new Color(1, 1, 1, 1);
        UIHp[1].color = new Color(1, 1, 1, 1);
        UIHp[2].color = new Color(1, 1, 1, 1);

        if (hp < 3)
            UIHp[2].color = new Color(1, 1, 1, 0.4f);
        if (hp < 2)
            UIHp[1].color = new Color(1, 1, 1, 0.4f);
        if (hp < 1)
            UIHp[0].color = new Color(1, 1, 1, 0.4f);
    }

}
