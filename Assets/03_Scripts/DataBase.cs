using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
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

public class DataBase : MonoBehaviour
{
    public static DataBase DB;

    public string path;

    private static List<string> dontDestroyObjects = new List<string>();
    public PlayerData playerData;

    private void Awake()
    {
        if (dontDestroyObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        DB = this;

        dontDestroyObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/jingsave";
        playerData = new PlayerData();
        playerData.itemDatas = new List<ItemData>();
    }

    public void SaveData(int slotIndex)
    {
        GalleryManager.galleryManager.SaveData();
        string JsonData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + slotIndex.ToString(), JsonData);
    }

    public void LoadData(int slotIndex)
    {
        string JsonData = File.ReadAllText(path + slotIndex.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(JsonData);
    }

    public void DataClear()
    {
        playerData = new PlayerData();
    }
}
