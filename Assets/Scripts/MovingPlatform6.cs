using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform6 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] platforms;

    void Start()
    {
        platforms = new MovingPlatform[5];
        platforms[0] = new MovingPlatform(GameObject.Find("MovingPlatform1") ,5, 1, 2);

        stageManager.platforms = platforms;
    }

    void Update()
    {
        stageManager.Move(platforms[0].platform, ref platforms[0].timer, platforms[0].time, platforms[0].direction, platforms[0].speed);
    }

}
