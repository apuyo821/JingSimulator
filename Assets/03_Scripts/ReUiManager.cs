using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReUiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIObjs;          // 반응형 Ui를 담은 배열

    public Text[] Texts;                // D-day, Hp, Mp의 Txt

    public Bar[] bars;                  // Hp, Mp를 구분하기 위한 클래스

    private void Start()
    {
        UISet(false);

        UISet(false);

        SetMaxHPMental(DataBase.DB.playerData.MaxHP, DataBase.DB.playerData.MaxMP);
        daySet(DataBase.DB.playerData.dDay);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            UISet(false);

        SetHPnMP(DataBase.DB.playerData.HP, DataBase.DB.playerData.MP);
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

    void UISet(bool p_flag)
    {
        for (int i = 0; i < UIObjs.Length; i++)
        {
            UIObjs[i].SetActive(p_flag);
        }
    }
}
