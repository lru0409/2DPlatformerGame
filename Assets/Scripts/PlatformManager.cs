using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class PlatformManager : MonoBehaviour
{
    public MovingPlatform[] platforms;

    void Start()
    {
        platforms = new MovingPlatform[5];
        platforms[0] = new MovingPlatform(GameObject.Find("MovingPlatform1") ,5, 1, 2);
    }

    void Update()
    {
        Move(platforms[0].platform, ref platforms[0].timer, platforms[0].time, platforms[0].direction, platforms[0].speed);
    }

    void Move(GameObject obj, ref float timer, float time, int direction, float speed)
    {
        if (obj.name != "Player")
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

    public void MovePlayer(Player player, GameObject platform)
    {
        for (int i = 0; i < platforms.Length; i++) {
            if (platforms[i].platform == platform) {
                Move(player.gameObject, ref platforms[i].timer, platforms[i].time, platforms[i].direction, platforms[i].speed);
            }
        }
    }

}
