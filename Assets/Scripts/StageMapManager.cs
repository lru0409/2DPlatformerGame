using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageMapManager : MonoBehaviour
{
	public Image[] lockedImages;
    public Image[] gradeImages;
    public Sprite[] starImages;

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

            if (GameManager.instance.stageGrade[i] == 0) {
                gradeImages[i].color = new Color(1, 1, 1, 0);
            } else {
                gradeImages[i].color = new Color(1, 1, 1, 1);
                if (GameManager.instance.stageGrade[i] == 1) {
                    gradeImages[i].sprite = starImages[0];
                } else if (GameManager.instance.stageGrade[i] == 2) {
                    gradeImages[i].sprite = starImages[1];
                } else {
                    gradeImages[i].sprite = starImages[2];
                }
            }
        }
    }

    public void StageClicked(int number)
    {
        if (GameManager.instance.stageOpened[number - 1] == false)
            return ;
        GameManager.instance.stageIndex = number - 1;
        SceneManager.LoadScene("Stage" + number + "Scene"); 
    }

}
