using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

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
        if (_indexData.Count == 0)
        {
            return null;
        }
        else
        {
            serializeText += _indexData[0].ToString();
            if (_indexData.Count > 1)
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
        {
            dummyData = new List<int>();
            return dummyData;
        }
    }

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        
        foreach (GameObject i in illustPanel)
        {
            i.SetActive(false);
        }
        //PlayerPrefs.DeleteAll();
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

                if(saveRefresh.EventIndex.Count > 0)
                {
                    for (int L = 0; L < saveRefresh.EventIndex.Count; L++)
                    {
                        bool isExist = false;

                        if(EventIndex.Count > 0)
                        {
                            for (int m = 0; m < EventIndex.Count; m++)
                            {
                                if(saveRefresh.EventIndex[L] == EventIndex[m])
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                        }
                        else
                        {

                        }
                        if (!isExist)
                        {
                            EventIndex.Add(saveRefresh.EventIndex[L]);
                        }
                    }
                }
            }
        }
        DataBase.DB.temporaryEndingData = EndingTypeIndex;
        DataBase.DB.temporaryEventData = EventIndex;
        string endingSerializedData = Serialize(DataBase.DB.temporaryEndingData);
        string eventSerializedData = Serialize(DataBase.DB.temporaryEventData);
        PlayerPrefs.SetString(EndingKey, endingSerializedData);
        PlayerPrefs.SetString(EventKey, eventSerializedData);
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
        DataBase.DB.playerData.EndingIndex = DataBase.DB.temporaryEndingData;
        DataBase.DB.playerData.EventIndex = DataBase.DB.temporaryEventData;
        string endingSerializedData = Serialize(DataBase.DB.temporaryEndingData);
        string eventSerializedData = Serialize(DataBase.DB.temporaryEventData);
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
                    EventIndex = new List<int>() { };
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
                    for (int i = 0; i < DataBase.DB.temporaryEndingData.Count; i++)
                    {
                        GalleryCards galleryCards = EndingCards[DataBase.DB.temporaryEndingData[i]].GetComponent<GalleryCards>();
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
                    for (int i = 0; i < DataBase.DB.temporaryEventData.Count; i++)
                    {
                        GalleryCards galleryCards = EventCards[DataBase.DB.temporaryEventData[i]].GetComponent<GalleryCards>();
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
