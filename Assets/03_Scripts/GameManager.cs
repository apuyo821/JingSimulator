using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public enum EBar
{
    HealthPoint,
    MentalPoint
}

[Serializable]
public class Bar
{
    public string Name;
    public EBar Type;
    public Slider Slider;

    public Bar(String name ,EBar type, Slider slider)
    {
        Name = name;
        Type = type;
        Slider = slider;
    }
}

public class GameManager : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject status_panel;
    public Text[] Texts;

    public Bar[] bars;

    private void Awake()
    {
        selectPanel.SetActive(false);
        status_panel.SetActive(false);
        daySet(DataBase.DB.playerData.dDay);
    }

    private void Start()
    {
        SetMaxHPMental(DataBase.DB.playerData.MaxHP, DataBase.DB.playerData.MaxMP);
    }

    private void Update()
    {
        SetHPnMP(DataBase.DB.playerData.HP, DataBase.DB.playerData.MP);

        if (DataBase.DB.playerData.HP > 50)
            DataBase.DB.playerData.HP = 50;
        if (DataBase.DB.playerData.MP > 25)
            DataBase.DB.playerData.MP = 25;
        if (DataBase.DB.playerData.HP < 0)
            DataBase.DB.playerData.HP = 0;
        if (DataBase.DB.playerData.MP < 0)
            DataBase.DB.playerData.MP = 0;
    }

    public void SetMaxHPMental(int _health, int _mental)
    {
        bars[0].Slider.maxValue = _health;
        bars[0].Slider.value = _health;
        bars[1].Slider.maxValue = _mental;
        bars[1].Slider.value = _mental;
    }

    public void SetHPnMP(int _HP, int _MP)
    {
        bars[0].Slider.value = _HP;
        bars[1].Slider.value = _MP;
    }

    public void daySet(int _dDay)
    {
        if (_dDay == 0)
        {
            Texts[0].text = "D-Day";
        }
        else
        {
            Texts[0].text = "D" + "-" + _dDay.ToString();
        }
    }
}
