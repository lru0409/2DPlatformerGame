using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager8 : MonoBehaviour
{
    public StageManager stageManager;
    public MovingPlatform[] movings;
    public FadingPlatform[] fadings;

    void Start()
    {
        movings = new MovingPlatform[5];
        movings[0] = new MovingPlatform(GameObject.Find("MovingPlatform1"), 5.2f, 1, 4);
        movings[1] = new MovingPlatform(GameObject.Find("MovingPlatform2"), 0.84f, 1, 3);
        movings[2] = new MovingPlatform(GameObject.Find("MovingPlatform3"), 0.84f, -1, 3);
        movings[3] = new MovingPlatform(GameObject.Find("MovingPlatform4"), 2.4f, -1, 6.5f);
        movings[4] = new MovingPlatform(GameObject.Find("MovingPlatform5"), 1.2f, -1, 7);
        stageManager.movings = movings;

        fadings = new FadingPlatform[4];
        fadings[0] = new FadingPlatform(GameObject.Find("FadingPlatform1"), new float[] {0.4f, 1, 1}, false);
        fadings[1] = new FadingPlatform(GameObject.Find("FadingPlatform2"), new float[] {0.4f, 1, 1}, true);
        fadings[2] = new FadingPlatform(GameObject.Find("FadingPlatform3"), new float[] {0.4f, 0.3f, 0.3f}, true);
        fadings[3] = new FadingPlatform(GameObject.Find("FadingPlatform4"), new float[] {0.4f, 0.3f, 0.3f}, false);
        stageManager.fadings = fadings;
    }

    void Update()
    {
        stageManager.MovePlatform();
        stageManager.FadePlatform();
    }
}
