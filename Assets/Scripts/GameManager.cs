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

    public Image[] UIHp;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartButton;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    public void NextStage()
    {
        // Calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;

        // Change Stage
        if (stageIndex < Stages.Length - 1) {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            UIStage.text = "STAGE " + (stageIndex + 1);
        } else { // Game Clear
            Time.timeScale = 0;
            Debug.Log("Game Clear");
            Text buttonText = UIRestartButton.GetComponentInChildren<Text>();
            buttonText.text = "CLEAR";
            UIRestartButton.SetActive(true);
        }
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
                PlayerReposition();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, 0);
        //Player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
