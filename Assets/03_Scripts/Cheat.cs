using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    [SerializeField] Text InputTxt;
    [SerializeField] InputField Input;

    int InputInt;
    ScheduleManager schedule;
    [SerializeField] buttonManager buttonManagerCs;
    [SerializeField] Text[] countText;
    [SerializeField] GameObject[] panels;
    [SerializeField] ScheduleManager scheduleManager;

    private void Start()
    {
        GameObject other = GameObject.Find("ScheduleManager");
        schedule = other.gameObject.GetComponent<ScheduleManager>();
        panels[0].SetActive(false);
        panels[1].SetActive(false);
    }


    public void ChangeDday()
    {
        InputInt = int.Parse(Input.text);
        DataBase.DB.playerData.dDay = int.Parse(Input.text);
        schedule.dDaySet(DataBase.DB.playerData.dDay);
        DataBase.DB.playerData.Day = (40 - DataBase.DB.playerData.dDay) + 7;
        if (DataBase.DB.playerData.Day > 31)
        {
            DataBase.DB.playerData.Day -= 31;
            DataBase.DB.playerData.Month++;
        }
        else if(DataBase.DB.playerData.Month == 8)
        {
            DataBase.DB.playerData.Month--;
        }
        if (DataBase.DB.playerData.Month > 8)
            DataBase.DB.playerData.Month = 8;
        if (DataBase.DB.playerData.Day % 7 == 0 || DataBase.DB.playerData.Day % 7 == 1 || DataBase.DB.playerData.Day % 7 == 2 || DataBase.DB.playerData.Day % 7 == 3)
            DataBase.DB.playerData.week = (DataBase.DB.playerData.Day % 7) + 3;
        else if(DataBase.DB.playerData.Day % 7 == 4 || DataBase.DB.playerData.Day % 7 == 5 || DataBase.DB.playerData.Day % 7 == 6)
            DataBase.DB.playerData.week = (DataBase.DB.playerData.Day % 7) - 4;
        schedule.MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
        
        if(DataBase.DB.playerData.dDay == 29 || DataBase.DB.playerData.dDay == 16 || DataBase.DB.playerData.dDay == 2 || DataBase.DB.playerData.dDay == 0)
            buttonManagerCs.btn[4].gameObject.SetActive(true);
        switch (DataBase.DB.playerData.dDay)
        {
            case 29:
                DataBase.DB.playerData.auditionIndex = 0;
                break;

            case 16:
                DataBase.DB.playerData.auditionIndex = 1;
                break;

            case 0:
                DataBase.DB.playerData.auditionIndex = 2;
                break;

            default:
                break;
        }
    }
    
    public void setStatusCount()
    {
        for (int i = 0; i < countText.Length; i++)
        {
            countNumSet(i);
        }
    }
    void countNumSet(int _num)
    {
        switch (_num)
        {
            case 0:
                countText[_num].text = "보컬 : " + DataBase.DB.playerData.vocalCount.ToString();
                break;

            case 1:
                countText[_num].text = "댄스 : " + DataBase.DB.playerData.danceCount.ToString();
                break;

            case 2:
                countText[_num].text = "방송 : " + DataBase.DB.playerData.broadcastCount.ToString();
                break;

            case 3:
                countText[_num].text = "게임 : " + DataBase.DB.playerData.gameCOunt.ToString();
                break;

            case 4:
                countText[_num].text = "운동 : " + DataBase.DB.playerData.GYMCount.ToString();
                break;

            case 5:
                countText[_num].text = "그림 : " + DataBase.DB.playerData.drawingCount.ToString();
                break;

            default:
                break;
        }
    }

    public void HpZero()
    {
        DataBase.DB.playerData.HP = 0;
    }

    public void MpZero()
    {
        DataBase.DB.playerData.MP = 0;
    }

    public void moneyPlus()
    {
        DataBase.DB.playerData.money += 99999;
    }

    public void strUp()
    {
        DataBase.DB.playerData.strength += 100;
    }

    public void deftUp()
    {
        DataBase.DB.playerData.deft += 100;
    }
    public void misukhmaUp()
    {
        DataBase.DB.playerData.misukham += 100;
    }

    public void actFlowTImeChange()
    {
        scheduleManager.actFlowTIme = 1f;
    }

    public void actChgTimeChange()
    {
        scheduleManager.actChgTime = 1f;
    }
}
