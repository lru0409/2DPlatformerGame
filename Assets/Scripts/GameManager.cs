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
    public bool[] stageOpened;
    public int[] stageGrade;
    public int[] totalPoint;

    public StageMapManager stageMapManager;

    void Start()
    {
        if (instance == null) {
            instance = this;
            instance.stageIndex = 0;
            instance.stageOpened = new bool[8] {true, false, false, false, false, true, true, true};
            instance.stageGrade = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
            instance.totalPoint = new int[8] {13, 21, 31, 29, 44, 70, 85, 90};
            DontDestroyOnLoad(this.gameObject);
        } else {
            if (instance != this)
                Destroy(this.gameObject);
        }
        stageMapManager.gameObject.SetActive(true);
    }

    public void UpdateDataToNextStage()
    {
        stageIndex++;
        stageOpened[stageIndex] = true;
        // stageGrade
    }
}