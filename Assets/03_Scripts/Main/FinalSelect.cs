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

        //행동 선택 시 알바 텍스트 비활성화
        if(ScheduleIndex == 2)
        {
            if(actType != 0)
            {
                WorkText.gameObject.SetActive(false);
            }
        }
    }

    void WeekCheck(int _actNum)
    {
        actType = _actNum;
        if (ScheduleIndex == 2)
        {
            if(DataBase.DB.playerData.week == 5 || DataBase.DB.playerData.week == 6 || DataBase.DB.playerData.week == 0)
            {
                if (actType== 31 || actType == 32 || actType == 33)
                {
                    //actType = actNum;
                    ScheduleManager.schedules[2] = actType;
                }
                else
                {
                    actType = 0;
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
    }

    //선택한 행동을 보여주는 함수
    void Update()
    {
        imageChange();
        image.sprite = selectImg[sprNum];
    }

    //switch문과 actType을 이용하여 스프라이트 변경하는 함수
    void imageChange()
    {
        switch (actType)
        {
            //empty
            case 0:
                sprNum = 0;
                break;
            
            //Workout
            case 11:
                sprNum = 1;
                break;

            //Drawing
            case 12:
                sprNum = 2;
                break;

            //VocalTraning
            case 13:
                sprNum = 3;
                break;

            //DanceTraning
            case 14:
                sprNum = 4;
                break;

            //ActTraining
            case 15:
                sprNum = 5;
                break;

            //SkinCare
            case 16:
                sprNum = 6;
                break;

            //미정
            case 17:
                sprNum = 7;
                break;

            //Rest
            case 21:
                sprNum = 8;
                break;

            //Stroll
            case 22:
                sprNum = 9;
                break;

            //Fan
            case 23:
                sprNum = 10;
                break;

            //Cheating
            case 24:
                sprNum = 11;
                break;

            //Hamburger
            case 31:
                sprNum = 12;
                break;

            //Store
            case 32:
                sprNum = 13;
                break;

            //DrawingAcademy
            case 33:
                sprNum = 14;
                break;

            default:
                break;
        }
    }
}