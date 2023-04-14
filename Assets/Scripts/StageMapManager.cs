using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMapManager : MonoBehaviour
{
    public GameManager gameManager;
    public Image[] lockedImages;

    void Start()
    {
        DisplayStageMap();
    }

    void Update()
    {
        
    }

    public void DisplayStageMap()
    {
        gameObject.SetActive(true);

        for(int i = 0; i < 12; i++)
        {
            if (gameManager.stageOpened[i] == true)
                Destroy(lockedImages[i]);
                //lockedImages[i].SetActive(false);
        }
    }

    public void StageClicked(int stageNumber)
    {
        if (gameManager.stageOpened[stageNumber - 1] == false)
            return ;
        gameManager.GoToStage(stageNumber);
        gameObject.SetActive(false);
    }
}
