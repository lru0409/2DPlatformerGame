using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageMapManager : MonoBehaviour
{
	public Image[] lockedImages;

	void Start()
	{
		DisplayStageMap();
	}

	public void DisplayStageMap()
    {
        for(int i = 0; i < 12; i++)
        {
            if (GameManager.instance.stageOpened[i] == true)
                Destroy(lockedImages[i]);
        }
    }

    public void StageClicked(int number)
    {
        if (GameManager.instance.stageOpened[number - 1] == false)
            return ;
        SceneManager.LoadScene("Stage" + number + "Scene"); 
    }

}