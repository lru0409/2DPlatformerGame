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
        movings = new MovingPlatform[4];
        movings[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 2.5f, 1, 3);
        movings[1] = new MovingPlatform(GameObject.Find("MovingPlatform2"), 2, -1, 3);
        movings[2] = new MovingPlatform(GameObject.Find("MovingPlatform3"), 3, -1, 3);
        stageManager.movings = movings;

        fadings = new FadingPlatform[4];
        fadings[0] = new FadingPlatform(GameObject.Find("FadingPlatform1"), new float[] {0.4f, 1, 1.5f}, false);
        fadings[1] = new FadingPlatform(GameObject.Find("FadingPlatform2"), new float[] {0.4f, 1, 1.5f}, true);
        fadings[2] = new FadingPlatform(GameObject.Find("FadingPlatform3"), new float[] {0.4f, 0.7f, 0.7f}, false);
        fadings[3] = new FadingPlatform(GameObject.Find("FadingPlatform4"), new float[] {0.4f, 0.7f, 0.7f}, true);
        stageManager.fadings = fadings;
    }

    void Update()
    {
        stageManager.MovePlatform();
        stageManager.FadePlatform();
    }
}
