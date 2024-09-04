using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GalleryManager : MonoBehaviour
{
    public static GalleryManager galleryManager;

    public GameObject[] illustPanel;

    public GameObject[] EndingCards;
    public GameObject[] EventCards;

    string EndingKey = "EndingNum";
    string EventKey = "EventNum";
    string serializeText, LoadDataText;

    public static List<int> EndingTypeIndex = new List<int>();
    public static List<int> EventIndex = new List<int>();
    //public List<int> savedInfo = new List<int>();
    List<int> dummyData;
    public SaveRefreshInfo saveRefresh = new SaveRefreshInfo();

    bool endingFirst = false;
    bool eventFirst = false;

    int j = 1;

    string path;

    private void Awake()
    {
        galleryManager = this;

        path = Application.persistentDataPath + "/jingsave";
    }

    public string Serialize(List<int> _indexData)
    {
        serializeText = "";
        if (_indexData.Count > 0)
        {
            serializeText += _indexData[0].ToString();
            if(_indexData.Count > 1)
            {
                do
                {
                    serializeText += "," + _indexData[j].ToString();
                    ++j;
                } while (j < _indexData.Count);
                //j를 1로 초기화
                j = 1;
            }
            return serializeText;
        }
        else
            return null;
    }

    public List<int> Deserialize(string data)
    {
        if (data.Length > 0)
        {
            dummyData = new List<int>();
            string[] parts = data.Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                dummyData.Add(int.Parse(parts[i]));
            }
            return dummyData;
        }
        else
            return null;
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        foreach (GameObject i in illustPanel)
        {
            i.SetActive(false);
        }

        setUp();
    }

    public void setUp()
    {
        LoadData(0);
        LoadData(1);
        for (int i = 1; i < 7; i++)
        {
            if (File.Exists(path + $"{i}"))
            {
                saveRefresh = new SaveRefreshInfo();
                saveRefresh = GetDummySave(i);
                if (saveRefresh.EndingIndex.Count > 0)
                {
                    for (int j = 0; j < saveRefresh.EndingIndex.Count; j++)
                    {
                        bool isExit = false;
                        if(EndingTypeIndex.Count > 0)
                        {
                            for (int k = 0; k < EndingTypeIndex.Count; k++)
                            {
                                if (saveRefresh.EndingIndex[j] == EndingTypeIndex[k])
                                {
                                    isExit = true;
                                    break;
                                }
                            }
                        }
                        else
                        {

                        }
                        if (!isExit)
                        {
                            EndingTypeIndex.Add(saveRefresh.EndingIndex[j]);
                        }
                    }
                }
            }
        }
        DataBase.DB.playerData.EndingIndex = EndingTypeIndex;
        string endingSerializedData = Serialize(DataBase.DB.playerData.EndingIndex);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString(EndingKey, endingSerializedData);
        PlayerPrefs.Save();
    }

    SaveRefreshInfo GetDummySave(int saveIndex)
    {
        string sss = File.ReadAllText(path + saveIndex.ToString());
        SaveRefreshInfo dummySaveData = JsonUtility.FromJson<SaveRefreshInfo>(sss);
        return dummySaveData;
    }

    public void SaveData()
    {
        string endingSerializedData = Serialize(DataBase.DB.playerData.EndingIndex);
        string eventSerializedData = Serialize(DataBase.DB.playerData.EventIndex);
        PlayerPrefs.SetString(EndingKey, endingSerializedData);
        PlayerPrefs.SetString(EventKey, eventSerializedData);
        PlayerPrefs.Save();
    }

    public void LoadData(int _index)
    {
        LoadDataText = "";
        if (PlayerPrefs.HasKey(EndingKey))
        {
            LoadDataText = "";
            switch (_index)
            {
                case 0:
                    EndingTypeIndex = new List<int>();
                    LoadDataText = PlayerPrefs.GetString("EndingNum");
                    EndingTypeIndex = Deserialize(LoadDataText);
                    break;

                case 1:
                    EventIndex = new List<int>();
                    LoadDataText = PlayerPrefs.GetString("EventNum");
                    EventIndex = Deserialize(LoadDataText);
                    break;

                default:
                    break;
            }
        }
    }

    public void show(int _index)
    {
        switch (_index)
        {
            case 0:
                if(endingFirst == false)
                {
                    for (int i = 0; i < DataBase.DB.playerData.EndingIndex.Count; i++)
                    {
                        GalleryCards galleryCards = EndingCards[EndingTypeIndex[i]].GetComponent<GalleryCards>();
                        galleryCards.ImageChange(1);
                    }
                    endingFirst = true;
                }
                else
                {

                }
                break;

            case 1:
                if (eventFirst == false)
                {
                    for (int i = 0; i < DataBase.DB.playerData.EventIndex.Count; i++)
                    {
                        GalleryCards galleryCards = EventCards[EventIndex[i]].GetComponent<GalleryCards>();
                        galleryCards.ImageChange(1);
                    }
                    eventFirst = true;
                }
                else
                {

                }
                break;

            default:
                break;
        }
    }
}
