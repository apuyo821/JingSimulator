using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerData
{
    //Audition Index
    public int auditionIndex = 0;

    //날짜 정보
    public int dDay = 50;
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
    public int game = 0;
}

public class DataBase : MonoBehaviour
{
    public static DataBase DB;

    //Audition Index
    public int auditionIndex = 0;

    /*
    //날짜 정보
    //public int dDay = 50;
    public int week = 3;        //0=Sunday, 1=Monday, 2=Tuesday, 3=Wednesday, 4=Thursday, 5=Friday, 6=Saturday
    public int Month = 7;
    public int Day = 7;

    //Main Status
    public int MaxHP = 50;
    public int HP = 50;
    public int MaxMP = 25;
    public int MP = 25;

    public int money = 0;

    public int deft = 0;        //1, 손재주
    public int vocal = 0;       //2, 보컬
    public int strength = 0;    //3, 근력
    public int rizz = 0;        //4, 매력
    public int dance = 0;       //5, 댄스
    public int misukham = 0;    //6, 미숙함
    public int game = 0;        //7, 게임력(표시는 안됨)
    */

    public string path;

    private static List<string> dontDestroyObjects = new List<string>();
    public PlayerData playerData = new PlayerData();
    //ScheduleManager schedule;

    int FileIndex=1;

    private void Awake()
    {
        if (dontDestroyObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        /*
        playerData.dDay = 50;
        playerData.week = 3;
        playerData.Month = 7;
        playerData.Day = 7;
        //HP MP Status
        playerData.MaxHP = 50;
        playerData.HP = 50;
        playerData.MaxMP = 25;
        playerData.MP = 25;
        //Main Status
        playerData.money = 0;
        playerData.deft = 0;
        playerData.vocal = 0;
        playerData.strength = 0;
        playerData.rizz = 0;
        playerData.dance = 0;
        playerData.misukham = 0;
        playerData.game = 0;
        */

        DB = this;

        dontDestroyObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/jingsave";
        /*
        if(GameObject.Find("ScheduleManager") != null)
        {
            GameObject other = GameObject.Find("ScheduleManager");
            schedule = other.gameObject.GetComponent<ScheduleManager>();
        }
        */
        Debug.Log(playerData.dDay);
    }

    public void SaveData(int slotIndex)
    {
        /*
        FileIndex = 1;
        //DBtoSaveData();
        for (int i = 0; i < 6; i++)
        {
            if (File.Exists(path + FileIndex))
            {
                Debug.Log("Save Exists");
                FileIndex++;
                Debug.Log(FileIndex);
            }
            else
            {
                string JsonData = JsonUtility.ToJson(playerData, true);
                File.WriteAllText(path + Slot.slot.nowSlot.ToString(), JsonData);
                Debug.Log("Save;");
                break;
                
            }
        }
        */
        string JsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + slotIndex.ToString(), JsonData);
    }

    public void LoadData(int slotIndex)
    {
        string JsonData = File.ReadAllText(path + slotIndex.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(JsonData);
        //SaveDatatoDB();
        //schedule.dDaySet(DB.playerData.Day);
        //schedule.MonthWeekSet(DB.playerData.week, DB.playerData.Month, DB.playerData.Day);
        //SceneManager.LoadScene("Main");
    }

    public void DataClear()
    {
        playerData = new PlayerData();
    }
}
