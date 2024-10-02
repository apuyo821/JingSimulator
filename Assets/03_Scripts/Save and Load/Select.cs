using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class SaveRefreshInfo
{
    //Audition Index
    public int auditionIndex = 0;

    //날짜 정보
    public int dDay = 40;
    public int week = 3;        //0=Sunday, 1=Monday, 2=Tuesday, 3=Wednesday, 4=Thursday, 5=Friday, 6=Saturday
    public int Month = 7;
    public int Day = 7;

    //Main Status
    public int MaxHP = 50;
    public int HP = 50;
    public int MaxMP = 25;
    public int MP = 25;

    public int money = 0;

    public int deft = 0;         //1, 손재주
    public int vocal = 0;        //2, 보컬
    public int strength = 0;     //3, 근력
    public int rizz = 0;         //4, 매력
    public int dance = 0;        //5, 댄스
    public int misukham = 0;      //6, 미숙함

    public int firstPlace = 0;
    public int rankScore = 0;

    public int danceCount = 0;
    public int vocalCount = 0;
    public int broadcastCount = 0;
    public int GYMCount = 0;
    public int gameCOunt = 0;
    public int drawingCount = 0;
    public int guitarCount = 0;

    public List<ItemData> itemDatas;
    public List<int> EndingIndex = new List<int>();
    public List<int> EventIndex = new List<int>();
}

public class Select : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public Text[] slotText;

    public GameObject warningPanel;
    public bool[] saveFile = new bool[6];
    public int slotNum;

    public string path;
    public SaveRefreshInfo saveRefreshInfo = new SaveRefreshInfo();

    private void Awake()
    {
        path = Application.persistentDataPath + "/jingsave";
    }

    public void OnEnable()
    {
        for (int i = 1; i < 7; i++)
        {
            if (File.Exists(path + $"{i}"))
            {
                saveFile[(i - 1)] = true;
                resetInfo(i);
                slotText[(i - 1)].text = "D-Day : " + saveRefreshInfo.dDay.ToString();
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
        DataClear();
        warningPanel.SetActive(false);
    }

    public void refreshInfo()
    {
        for (int i = 1; i < 7; i++)
        {
            if (File.Exists(path + $"{i}"))
            {
                saveFile[(i - 1)] = true;
                resetInfo(i);
                slotText[(i - 1)].text = "D-Day : " + saveRefreshInfo.dDay.ToString();
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
        DataClear();
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

    void resetInfo(int slotIndex)
    {
        string sss = File.ReadAllText(path + slotIndex.ToString());
        saveRefreshInfo = JsonUtility.FromJson<SaveRefreshInfo>(sss);
    }

    void DataClear()
    {
        saveRefreshInfo = new SaveRefreshInfo();
    }
}
