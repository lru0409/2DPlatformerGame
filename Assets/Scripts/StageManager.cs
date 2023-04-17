using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

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

    // Platforms
    public MovingPlatform[] movings;
    public FadingPlatform[] fadings;

    void Start()
    {
        SetHp(3);
        UIStage.text = "STAGE " + (GameManager.instance.stageIndex + 1);
        Time.timeScale = 1;
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
            if (collision.gameObject.layer == 6) { // Player
                HpDown();
            }
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

    public void ContinuePlay() // 이어서 하기
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void Restart() // 다시 시작하기
    {
        SceneManager.LoadScene("Stage" + (GameManager.instance.stageIndex + 1) + "Scene");
    }

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

    public void GoToPlayStage() // 다음 스테이지로 or 다시 시도하기
    {
        EventUI.SetActive(false);

        Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
        if (playButtonText.text == "다음 스테이지로")
            GameManager.instance.UpdateDataToNextStage();
        SceneManager.LoadScene("Stage" + (GameManager.instance.stageIndex + 1) + "Scene");
    }

    public void GoToStageMap() // 스테이지 맵으로
    {
        if (EventUI.activeSelf == true) {
            Text playButtonText = UIEventPlayButton.GetComponentInChildren<Text>();
            if (playButtonText.text == "다음 스테이지로") {
                GameManager.instance.UpdateDataToNextStage();
                EventUI.SetActive(false);
            }
        } else {  // Pause UI
            PauseUI.SetActive(false);
        }
        SceneManager.LoadScene("StageMapScene");
    }

    // ----- Variable Platform -----

    public void MovePlatform()
    {
        for (int i = 0; i < movings.Length; i++) {
            Move(movings[i].platform, "MovingPlatform", ref movings[i].timer, movings[i].time, movings[i].direction, movings[i].speed);
        }
    }

    public void Move(GameObject obj, string tag, ref float timer, float time, int direction, float speed)
    {
        if (tag == "MovingPlatform")
            timer += Time.deltaTime;
        if (timer < time) {
            if (direction == 1)
                obj.transform.position = obj.transform.position + (Vector3.right * speed) * Time.deltaTime;
            else
                obj.transform.position = obj.transform.position + (Vector3.left * speed) * Time.deltaTime;
        } else if (timer < time * 2) {
            if (direction == 1)
                obj.transform.position = obj.transform.position + (Vector3.left * speed) * Time.deltaTime;
            else
                obj.transform.position = obj.transform.position + (Vector3.right * speed) * Time.deltaTime;
        } else {
            timer = 0;
        }
    }

    public void FadePlatform()
    {
        for (int i = 0; i < fadings.Length; i++) {
            fadings[i].timer += Time.deltaTime;
            
            // if (fadings[i].timer < fadings[i].time[0]) {
            //     if (fadings[i].fade == true)
            //         Debug.Log("platform unfade");
            //     else
            //         Debug.Log("platform fade");
            // } else if (fadings[i].timer < fadings[i].time[0] + fadings[i].time[1]) {
            //     if (fadings[i].fade == true)
            //         Debug.Log("platform fade");
            //     else
            //         Debug.Log("platform unfade");
            // } else {
            //     fadings[i].timer = 0;
            // }
        }
    }

//     public void Fade(bool fade, GameObject platform)
//     {
//         if (fade == true) {

//             platform.transform.GetComponent<
//         } else {  // Unfade

//         }
//     }
}

public struct MovingPlatform
{
    public GameObject platform;
    public float timer;
    public float time;
    public int direction;
    public float speed;

    public MovingPlatform(GameObject platform, float time, int direction, float speed) {
        this.platform = platform;
        this.timer = 0;
        this.time = time;
        this.direction = direction;
        this.speed = speed;
    }
}

public struct FadingPlatform
{
    public GameObject platform;
    public float timer;
    public float[] time; // 상태변경시간, 유지시간, 유지시간
    public bool fade;

    public FadingPlatform(GameObject platform, float[] time, bool fade) {
        this.platform = platform;
        this.timer = 0;
        this.time = time;
        this.fade = fade;

        Tilemap tilemap = platform.GetComponent<Tilemap>();
        TilemapCollider2D collider = platform.GetComponent<TilemapCollider2D>();
        if (fade == true) {
            tilemap.color = new Color(1, 1, 1, 1);
            collider.enabled = true;
        } else {
            tilemap.color = new Color(1, 1, 1, 0);
            collider.enabled = false;
        }
    }
}
