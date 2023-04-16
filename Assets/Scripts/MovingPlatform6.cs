using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform6 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] platforms;

    void Start()
    {
        platforms = new MovingPlatform[6];
        platforms[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 4.5f, -1, 2.5f);
        platforms[1] = new MovingPlatform(GameObject.Find("MovingPlatform2"), 2, 1, 2.5f);
        platforms[2] = new MovingPlatform(GameObject.Find("MovingPlatform3"), 2, -1, 2.5f);
        platforms[3] = new MovingPlatform(GameObject.Find("MovingPlatform4"), 4, 1, 3f);
        stageManager.platforms = platforms;
    }

    void Update()
    {
        stageManager.MovePlatform();
    }
}
