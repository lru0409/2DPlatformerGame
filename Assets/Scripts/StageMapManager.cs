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
        // 해당 스테이지가 잠겨있다면 아무것도 하지 않기
        // 열려있다면 StageMap 비활성화하고 해당 스테이지로 이동
    }
}
