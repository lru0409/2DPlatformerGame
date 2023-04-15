using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Player player;

    public int point;
    public int hp = 3;

    // GameUI
    public Image[] UIHp;
    public Text UIStage;

    // EventUI
    public GameObject EventUI;
    public Text UIEventTitle;
    public Button UIEventPlayButton;

    // PauseUI
    public GameObject PauseUI;

    void Start()
    {
        SetHp(3);
    }

    public void ClearStage()
    {
        Time.timeScale = 0;
        if (GameManager.instance.stageIndex < 11) {
            DisplayEventUI("Stage Clear!");
        } else {
            Debug.Log("Clear All Stage");
        }
    }

    public void HpDown()
    {
        if (hp > 0) {
            SetHp(hp - 1);
        }
        if (hp == 0) {
            player.OnDie();
            DisplayEventUI("Fail to Clear");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            HpDown();
            if (hp > 0) {
                player.Reposition();
            }
        }
    }

    void SetHp(int newHp)
    {
        hp = newHp;

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

    // ----- Stage Pause -----

    public void DisplayPauseUI()
    {
        Time.timeScale = 0;
        PauseUI.SetActive(true);
    }

    public void ContinuePlay()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Stage" + GameManager.instance.stageIndex + "Scene");
    }

    // 이어서하기, 다시 시작하기, 스테이지 맵으로

    // ----- Stage Event -----

    public void DisplayEventUI(string title)
    {
        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();

        UIEventTitle.text = title;
        if (title == "Stage Clear!") {
            playButtonText.text = "다음 스테이지로";
        } else { // title = "Fail to Clear"
            playButtonText.text = "다시 시도하기";
        }
        EventUI.SetActive(true);
    }

    public void GoToPlayStage()
    {
        EventUI.SetActive(false);

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로")
            GameManager.instance.UpdateDataToNextStage();
        SceneManager.LoadScene("Stage" + (GameManager.instance.stageIndex + 1) + "Scene");
    }

    public void GoToStageMap()
    {
        if (EventUI.activeSelf == true)
        {
            Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
            if (playButtonText.text == "다음 스테이지로") {
                GameManager.instance.UpdateDataToNextStage();
                EventUI.SetActive(false);
            } else {  // Pause UI
                PauseUI.SetActive(false);
            }
        }
        SceneManager.LoadScene("StageMapScene");
    }

}
