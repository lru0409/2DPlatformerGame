using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject stageMap;
    public bool[] stageOpened = {true, false, false, false, false, false, false, false, false, false, false, false};
    public Image[] lockedImages;

    void Start()
    {
        DisplayStageMap();
    }

    public void DisplayStageMap()
    {
        for(int i = 0; i < 12; i++)
        {
            if (stageOpened[i] == true)
                Destroy(lockedImages[i]);
        }
    }

    public void StageClicked(int number)
    {
        if (stageOpened[number - 1] == false)
            return ;
        SceneManager.LoadScene("Stage" + number + "Scene"); 
    }

}
