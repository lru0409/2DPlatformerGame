using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMapManager : MonoBehaviour
{
    public GameManager gameManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StageClicked(int stageNumber)
    {
        if (gameManager.stageOpened[stageNumber - 1] == false)
            return ;
        gameManager.GoToStage(stageNumber);
        gameObject.SetActive(false);
    }
}
