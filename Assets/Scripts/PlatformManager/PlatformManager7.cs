using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager7 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] movings;
    public DisappearingPlatform[] disappearings;

    void Start()
    {
        movings = new MovingPlatform[1];
        movings[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 4.5f, -1, 2.5f);
        stageManager.movings = movings;

        disappearings = new DisappearingPlatform[1];
        disappearings[0] = new DisappearingPlatform(GameObject.Find("DisappearingPlatform1"), {3, 3}, true);
        stageManager.disappearings = disappearings;
    }

    void Update()
    {
        stageManager.MovePlatform();
        stageManager.DisappearPlatform();
    }
}
