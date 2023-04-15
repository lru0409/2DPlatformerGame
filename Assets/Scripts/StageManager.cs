using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Player player;
    // public GameObject gameManagerObject;
    // public GameManager gameManager;

    public int point;
    public int hp = 3;

    // GameUI
    public Image[] UIHp;
    public Text UIPoint;
    public Text UIStage;

    // EventUI
    public GameObject StageEventUI;
    public Text UIEventTitle;
    public Button UIEventPlayButton;

    void Start()
    {
        // gameManagerObject = GameObject.Find("GameManager");
        // gameManager = gameManagerObject.GetComponent<GameManager>();
        SetHp(3);
    }

    void Update()
    {
        UIPoint.text = point.ToString(); // 이거 어떻게 좀 바꿔보자
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

    // ----- EventUI -----

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

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로")
            GameManager.instance.UpdateDataToNextStage();
        SceneManager.LoadScene("Stage" + (GameManager.instance.stageIndex + 1) + "Scene");
    }

    public void GoToStageMap()
    {
        StageEventUI.SetActive(false);

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로") {
            GameManager.instance.UpdateDataToNextStage();
        }
        SceneManager.LoadScene("StageMapScene");
    }

}
