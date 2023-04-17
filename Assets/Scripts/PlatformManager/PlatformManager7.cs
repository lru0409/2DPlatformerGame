using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformManager7 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] movings;
    public FadingPlatform[] fadings;

    void Start()
    {
        movings = new MovingPlatform[1];
        movings[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 4.5f, -1, 2.5f);
        stageManager.movings = movings;

        fadings = new FadingPlatform[1];
        fadings[0] = new FadingPlatform(GameObject.Find("FadingPlatform1"), new float[] {1, 3, 3}, true);
        stageManager.fadings = fadings;
    }

    void Update()
    {
        stageManager.MovePlatform();
        stageManager.FadePlatform();

        //test(3);

    }

    // float timer = 0;
    // int status = -1; // fade : -1, unfade : 1

    // void test(float time)
    // {
    //     Tilemap tilemap = fadings[0].platform.GetComponent<Tilemap>();
    //     TilemapCollider2D collider = fadings[0].platform.GetComponent<TilemapCollider2D>();

    //     timer += Time.deltaTime;

    //     if (timer <= 2 && status == -1) {
    //         tilemap.color = new Color(1, 1, 1, 1 - timer/2);
    //     } else if (timer <= 2 && status == 1) {
    //         tilemap.color = new Color(1, 1, 1, timer/2);
    //     } else {
    //         if (status == 1)
    //             collider.enabled = true;
    //         else
    //             collider.enabled = false;
    //         if (timer > 5) {
    //             status *= -1;
    //             timer = 0;
    //         }
    //     }
    // }

}
