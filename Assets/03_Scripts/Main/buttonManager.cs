using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public Slider[] slider;
    public Button[] btn;

    //status text
    public Text[] Texts;

    public GameObject[] UIs;

    private void Update()
    {
        if (DataBase.DB.playerData.dDay == 29 || DataBase.DB.playerData.dDay == 16 || DataBase.DB.playerData.dDay == 2 || DataBase.DB.playerData.dDay == 0)
            btn[4].gameObject.SetActive(true);
    }

    public void falseBtnItr()
    {
        for (int i = 0; i < btn.Length; i++)
            btn[i].interactable = false;
    }

    public void trueBtnItr()
    {
        for (int i = 0; i < btn.Length; i++)
            btn[i].interactable = true;
    }

    //스탯창 버튼을 클릭했을 때, 스탯을 보여주는 함수
    //굳이 Update에 넣을 필요가 없으니, 스탯창을 클릭했을 때 마다
    public void showStats()
    {
        Texts[0].text = "손재주 : " + DataBase.DB.playerData.deft.ToString();
        Texts[1].text = "노래 및 발성 : " + DataBase.DB.playerData.vocal.ToString();
        Texts[2].text = "근력 : " + DataBase.DB.playerData.strength.ToString();
        Texts[3].text = "매력 : " + DataBase.DB.playerData.rizz.ToString();
        Texts[4].text = "댄스 : " + DataBase.DB.playerData.dance.ToString();
        Texts[5].text = "미숙함 : " + DataBase.DB.playerData.misukham.ToString();

        slider[0].value = DataBase.DB.playerData.deft;
        slider[1].value = DataBase.DB.playerData.vocal;
        slider[2].value = DataBase.DB.playerData.strength;
        slider[3].value = DataBase.DB.playerData.rizz;
        slider[4].value = DataBase.DB.playerData.dance;
        slider[5].value = DataBase.DB.playerData.misukham;
    }
    public void scene_to_start_option()
    {
        SceneManager.LoadScene("Start_option_menu");
    }

    public void scene_to_title()
    {
        SceneManager.LoadScene("Title");
    }

    public void scene_to_Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void SchBtnIntrFalse()
    {
        btn[0].interactable = false;
    }

    public void setInvenExplainObj(bool p_flag)
    {
        for (int i = 0; i < 3; i++)
        {
            UIs[i].SetActive(p_flag);
        }
    }

    public void uiControl(bool p_flag)
    {
        for (int i = 3; i < 8; i++)
        {
            UIs[i].SetActive(p_flag);
        }
    }
}
