using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int stageIndex = 0;
    public bool[] stageOpened = {true, false, false, false, false, false, false, false, false, false, false, false};

    void Start()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public void UpdateDataToNextStage()
    {
        stageIndex++;
        stageOpened[stageIndex] = true;
    }
    
}
