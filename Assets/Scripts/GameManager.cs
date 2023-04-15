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
    public Image[] lockedImages;

    void Start()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            if (instance != this)
                Destroy(this.gameObject);
        }

        DisplayStageMap();
    }

    public void UpdateDataToNextStage()
    {
        stageIndex++;
        stageOpened[stageIndex] = true;
    }

    public void DisplayStageMap()
    {
        for(int i = 0; i < 12; i++)
        {
            Debug.Log("i: " + i + "    opened: " + stageOpened[i]);
            if (stageOpened[i] == true)
                Destroy(lockedImages[i]);
        }
    }

    public void StageClicked(int number)
    {
        for(int i = 0; i < 12; i++)
        {
            Debug.Log("i: " + i + "    opened: " + stageOpened[i]);
        }

        if (stageOpened[number - 1] == false)
            return ;
        SceneManager.LoadScene("Stage" + number + "Scene"); 
        //DontDestroyOnLoad(gameObject);
    }

}
