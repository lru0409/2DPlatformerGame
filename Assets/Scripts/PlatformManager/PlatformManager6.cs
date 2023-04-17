using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager6 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] movings;

    void Start()
    {
        movings = new MovingPlatform[6];
        movings[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 4.5f, -1, 2.5f);
        movings[1] = new MovingPlatform(GameObject.Find("MovingPlatform2"), 2, 1, 2.5f);
        movings[2] = new MovingPlatform(GameObject.Find("MovingPlatform3"), 2, -1, 2.5f);
        movings[3] = new MovingPlatform(GameObject.Find("MovingPlatform4"), 4, 1, 3f);
        stageManager.movings = movings;
    }

    void Update()
    {
        stageManager.MovePlatform();
    }
}
