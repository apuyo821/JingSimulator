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
        Texts[1].text = "보컬 : " + DataBase.DB.playerData.vocal.ToString();
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

    public void ShowWorkText()
    {
        if(DataBase.DB.playerData.week == 5 || DataBase.DB.playerData.week == 6 || DataBase.DB.playerData.week == 0)
        {
            Texts[6].gameObject.SetActive(true);
        }
        else
        {
            Texts[6].gameObject.SetActive(false);
        }
    }

    /*
    public void Save()
    {
        DataBase.DB.SaveData();
    }
    */
}
