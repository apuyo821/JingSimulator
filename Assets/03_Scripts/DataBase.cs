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

    public int danceCount = 0;
    public int vocalCount = 0;
    public int broadcastCount = 0;
    public int GYMCount = 0;
    public int gameCOunt = 0;
    public int drawingCount = 0;

    public List<ItemData> itemDatas;
    public List<int> EndingIndex = new List<int>();
    public List<int> EventIndex = new List<int>();

    public bool isGYMEvent = false;
    public bool isGameEvent = false;
    public bool isDrawingEvent = false;
}

public class DataBase : MonoBehaviour
{
    public static DataBase DB;

    public string path;

    public static List<string> dontDestroyObjects = new List<string>();
    public PlayerData playerData;
    public List<int> temporaryEndingData = new List<int>();
    public List<int> temporaryEventData = new List<int>();

    public bool isAuditionEnd = false;

    public bool thirdAudition = false;  //true == 합격, false == 불합격

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

        isAuditionEnd = false;
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
        isAuditionEnd = false;
        playerData = new PlayerData();
        playerData.itemDatas = new List<ItemData>();
    }

    private void Update()
    {
        if (playerData.deft < 0)
            playerData.deft = 0;
        else if (playerData.strength < 0)
            playerData.strength = 0;
        else if (playerData.vocal < 0)
            playerData.vocal = 0;
        else if (playerData.rizz < 0)
            playerData.rizz = 0;
        else if (playerData.dance < 0)
            playerData.dance = 0;
        else if (playerData.misukham < 0)
            playerData.misukham = 0;
    }
}

