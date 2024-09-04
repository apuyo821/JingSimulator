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

    private void Start()
    {
        GameObject other = GameObject.Find("ScheduleManager");
        schedule = other.gameObject.GetComponent<ScheduleManager>();
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
}
