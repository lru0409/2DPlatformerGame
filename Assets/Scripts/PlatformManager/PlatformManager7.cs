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
        fadings[0] = new FadingPlatform(GameObject.Find("FadingPlatform1"), new float[] {0.5f, 2, 2}, true);
        stageManager.fadings = fadings;
    }

    void Update()
    {
        stageManager.MovePlatform();
        stageManager.FadePlatform();
    }
}
