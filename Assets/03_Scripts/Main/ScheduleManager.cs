using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScheduleManager : MonoBehaviour
{
    [SerializeField]    float actFlowTIme;
    [SerializeField]    float actChgTime;
    [SerializeField]    int daycount = 0;
    public static int[] schedules;
    public CinemachineVirtualCamera[] vCams;
    public static bool isActing;

    buttonManager buttonManager;
    public GameObject panel_select_task;
    public Text dDayTxT;
    public Text MonthWeekText;
    string WeekName;

    //초기값 초기화
    void Awake()
    {
        schedules = new int[3];
        isActing = false;
    }

    private void Start()
    {
        GameObject btmObject = GameObject.Find("ButtonManager");
        buttonManager = btmObject.gameObject.GetComponent<buttonManager>();
        if (DataBase.DB.playerData.HP < 1)
        {
            buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
        }
        dDaySet(DataBase.DB.playerData.dDay);
        MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
    }

    //D-Day 설정
    public void dDaySet(int _dDay)
    {
        
        if (_dDay == 0)
        {
            dDayTxT.text = "D-Day";
        }
        else
        {
            dDayTxT.text = "D" + "-" + _dDay.ToString();
        }
    }

    public void MonthWeekSet(int _week, int _month, int _day)
    {
        switch (_week)
        {
            case 0:
                WeekName = "일요일";
                break;
            case 1:
                WeekName = "월요일";
                break;
            case 2:
                WeekName = "화요일";
                break;
            case 3:
                WeekName = "수요일";
                break;
            case 4:
                WeekName = "목요일";
                break;
            case 5:
                WeekName = "금요일";
                break;
            case 6:
                WeekName = "토요일";
                break;
            default:
                break;
        }
        MonthWeekText.text = _month + "월 " + _day + "일 " + WeekName;
    }

    public void MonthCount()
    {
        if(DataBase.DB.playerData.Day == 31)
        {
            DataBase.DB.playerData.Day = 1;
            DataBase.DB.playerData.Month++;
        }
        else
        {
            DataBase.DB.playerData.Day++;
        }
    }

    //일과 진행
    public void Processing()
    {
        //하나라도 선택되지 않았다면 일과가 시작 되지 않음
        if (schedules[0] != 0 &&
           schedules[1] != 0 &&
           schedules[2] != 0)
        {
            //일과 선택 패널 숨김
            GameObject.Find("panel_select_task").SetActive(false);

            //행동 진행 중 다른 버튼 비활성화
            buttonManager.falseBtnItr();

            //행동 시작
            StartCoroutine(Process(schedules[daycount]));
        }
    }
    //10초의 시간 동안 행동 진행
    IEnumerator Process(int _actNum)
    {
        switch (_actNum)
        {
            //empty
            case 0:
                break;

            //workout
            case 11:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.strength += 2;
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 1;
                daycount++;
                break;

            //drawing
            case 12:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.deft += 2;
                DataBase.DB.playerData.HP -= 1;
                daycount++;
                break;

            //vocalTraning
            case 13:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.rizz++;
                DataBase.DB.playerData.vocal++;
                DataBase.DB.playerData.HP -= 2;
                DataBase.DB.playerData.MP -= 1;
                Debug.Log(DataBase.DB.playerData.vocal);
                daycount++;
                break;

            //danceTraning
            case 14:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.rizz++;
                DataBase.DB.playerData.dance++;
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 1;
                daycount++;
                break;

            //actTraining
            case 15:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.rizz += 2;
                DataBase.DB.playerData.HP -= 2;
                DataBase.DB.playerData.MP -= 2;
                daycount++;
                break;

            //skinCare
            case 16:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.rizz += 4;
                DataBase.DB.playerData.MP += 1;
                daycount++;
                break;

            //미정
            case 17:
                vCams[_actNum - 10].Priority = 11;
                daycount++;
                break;

            //Game
            case 21:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP += 4;
                DataBase.DB.playerData.MP += 3;
                DataBase.DB.playerData.game += 2;
                daycount++;
                break;

            //WalkPark
            case 22:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP += 1;
                DataBase.DB.playerData.MP += 5;
                daycount++;
                break;

            //Fan
            case 23:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP += 1;
                DataBase.DB.playerData.MP += 4;
                daycount++;
                break;

            //Cheating
            case 24:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP += 5;
                DataBase.DB.playerData.MP += 4;
                DataBase.DB.playerData.rizz -= 2;
                daycount++;
                break;

            //Hamburger
            case 31:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 2;
                DataBase.DB.playerData.money += 400;
                DataBase.DB.playerData.deft += 1;
                daycount++;
                break;

            //Store
            case 32:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP -= 1;
                DataBase.DB.playerData.MP -= 2;
                DataBase.DB.playerData.money += 200;
                daycount++;
                break;

            //Drawing Academy
            case 33:
                vCams[_actNum - 10].Priority = 11;
                DataBase.DB.playerData.HP -= 2;
                DataBase.DB.playerData.MP -= 2;
                DataBase.DB.playerData.money += 300;
                DataBase.DB.playerData.deft += 1;
                daycount++;
                break;

            default:
                break;
        }

        yield return new WaitForSeconds(actFlowTIme); //행동 진행 시간
        vCams[_actNum - 10].Priority = 5;
        yield return new WaitForSeconds(actChgTime); //배경 전환 시간, 집으로 카메라 바뀌었다가 행동 배경으로 전환
        
        if(daycount < 3)
        {
            //daycount가 3 보다 작으면, 남은 행동 진행
            StartCoroutine(Process(schedules[daycount]));
        }
        else
        {
            //datcount가 3 이상이면 하루 스케쥴 종료 및 스탯, 돈, 날짜 정산
            daycount = 0;
            DataBase.DB.playerData.dDay--;
            WeekCount();
            dDaySet(DataBase.DB.playerData.dDay);
            MonthCount();
            MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
            for (int index = 0; index < schedules.Length; index++)
            {
                schedules[index] = 0;
            }
            buttonManager.trueBtnItr();

            //오디션 이벤트 발생
            if (DataBase.DB.playerData.dDay == 39 || DataBase.DB.playerData.dDay == 26 || DataBase.DB.playerData.dDay == 19)
            {
                //fist, second, third Audition
                buttonManager.btn[0].interactable = false;
                buttonManager.btn[4].gameObject.SetActive(true);
            }
            else if(DataBase.DB.playerData.dDay == 0)
            {
                //Final Audition
                buttonManager.btn[0].interactable = false;
                buttonManager.btn[4].gameObject.SetActive(true);
                buttonManager.btn[5].gameObject.SetActive(true);
            }
            //HP가 0일 때 휴식 이벤트
            if (DataBase.DB.playerData.HP < 1)
            {
                buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
            }
        }
    }

    public void WeekCount()
    {
        DataBase.DB.playerData.week++;
        if (DataBase.DB.playerData.week == 7)
        {
            DataBase.DB.playerData.week = 0;
        }
    }

    //오디션 진행
    public void AuditionPricessing()
    {
        SceneManager.LoadScene("auditionScene");
    }

    public void CheckHpMP()
    {
        if(DataBase.DB.playerData.HP == 0 || DataBase.DB.playerData.MP == 0)
        {
            StartCoroutine(IsZero());
        }
        else
        {
            panel_select_task.SetActive(true);
        }
    }

    //체력 or MP가 0일 때의 이벤트
    IEnumerator IsZero()
    {
        vCams[20].Priority = 11;
        DataBase.DB.playerData.dDay--;
        DataBase.DB.playerData.week++;
        MonthCount();
        DataBase.DB.playerData.HP += 25;
        DataBase.DB.playerData.MP += 13;
        yield return new WaitForSeconds(4.0f);
        vCams[20].Priority = 5;
        dDaySet(DataBase.DB.playerData.dDay);
        MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
        buttonManager.btn[0].GetComponentInChildren<Text>().text = "스케쥴";
    }
}