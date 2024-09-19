using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class FinalSelect : MonoBehaviour, IDropHandler
{
    public int ScheduleIndex;
    public int actType;
    public Image image;
    public Sprite[] selectImg;
    [SerializeField] int sprNum;
    [SerializeField] Text WorkText;

    


    //행동들을 FinalChoice 칸에 드랍 했을 때, 행동의 종류를 초기화하는 함수
    public void OnDrop(PointerEventData eventData)
    {
        dragObj dragObj = eventData.pointerDrag.gameObject.GetComponent<dragObj>();
        
        
        //행동 오브젝트의 행동 종류를 finalselect 각각의 actType에 저장
        WeekCheck(dragObj.actNum);

        /*
        //행동 선택 시 알바 텍스트 비활성화
        //원래는 금토일이되면은 알바를 해야한다고 알려주기 위해 알바 텍스트를 띄웠지만, 알바가 하나로 줄어들었음에 따라 그럴 필요가 없어짐
        if(ScheduleIndex == 2)
        {
            if(actType != 0)
            {
                WorkText.gameObject.SetActive(false);
            }
        }
        */
    }

    void WeekCheck(int _actNum)
    {
        actType = _actNum;
        if (ScheduleIndex == 2)
        {
            if(DataBase.DB.playerData.week == 5 || DataBase.DB.playerData.week == 6 || DataBase.DB.playerData.week == 0)
            {
                if (actType != 8)
                {
                    actType = 8;
                    //actType = actNum;
                    ScheduleManager.schedules[2] = actType;
                }
                else
                {
                    ScheduleManager.schedules[2] = actType;
                }
            }
        }
        ScheduleManager.schedules[ScheduleIndex] = actType;
    }

    //FinalChoice 칸을 클릭하면(button 사용) 선택된 행동을 취소하는 함수 & selectPanel의 행동 선택 취소 버튼
    public void clickResetIndex()
    {
        //button에 쓰이는 함수
        actType = 0;
        ScheduleManager.schedules[ScheduleIndex] = actType;
    }

    //결정 버튼을 눌렀을 때 행동 결정 패널에서 이미지 초기화 하기
    public void resetActType()
    {
        actType = 0;
        image.sprite = selectImg[actType];
    }

    //선택한 행동을 보여주는 함수
    void Update()
    {
        image.sprite = selectImg[actType];
        //원래는 금토일이되면은 알바를 해야한다고 알려주기 위해 알바 텍스트를 띄웠지만, 알바가 하나로 줄어들었음에 따라 알바 고정 시키기 용도
        /*
        if (ScheduleIndex == 2)
        {
            if (DataBase.DB.playerData.week == 5 || DataBase.DB.playerData.week == 6 || DataBase.DB.playerData.week == 0)
            {
                if (actType != 0)
                {
                    WorkText.gameObject.SetActive(false);
                }
                else
                {
                    WorkText.gameObject.SetActive(true);
                }
            }
        }
        */
        if (ScheduleIndex == 2)
        {
            if (DataBase.DB.playerData.week == 5 || DataBase.DB.playerData.week == 6 || DataBase.DB.playerData.week == 0)
            {
                if (ScheduleIndex == 2)
                    actType = 8;
                ScheduleManager.schedules[2] = 8;
            }
        }
    }
}