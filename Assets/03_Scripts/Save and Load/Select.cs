using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Select : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public Text[] slotText;

    public GameObject warningPanel;
    public bool[] saveFile = new bool[6];
    public int slotNum;
    public ScheduleManager schedule;

    public void OnEnable()
    {
        for (int i = 1; i < 7; i++)
        {
            if (File.Exists(DataBase.DB.path + $"{i}"))
            {
                saveFile[(i - 1)] = true;
                DataBase.DB.LoadData(i);
                slotText[(i-1)].text = "D-Day : " + DataBase.DB.playerData.dDay.ToString() + "\r\n​ 세이브" + i.ToString();
            }
            else
            {
                slotText[(i - 1)].text = "No Save";
                if(gameObject.name == "Load")
                {
                    buttons[(i - 1)].interactable = false;
                }
            }
        }
        DataBase.DB.DataClear();
    }

    public void refreshInfo()
    {
        for (int i = 1; i < 7; i++)
        {
            if (File.Exists(DataBase.DB.path + $"{i}"))
            {
                saveFile[(i - 1)] = true;
                DataBase.DB.LoadData(i);
                slotText[(i - 1)].text = "D-Day : " + DataBase.DB.playerData.dDay.ToString() + "\r\n​ 세이브" + i.ToString();
            }
            else
            {
                slotText[(i - 1)].text = "No Save";
                if (gameObject.name == "Load")
                {
                    buttons[(i - 1)].interactable = false;
                }
            }
        }
        DataBase.DB.DataClear();
    }

    public void LoadData()
    {
        DataBase.DB.LoadData(slotNum);
        GoGame();
    }

    public void GoGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SaveData()
    {
        DataBase.DB.SaveData(slotNum);
        warningPanel.SetActive(false);
        refreshInfo();
    }
}
