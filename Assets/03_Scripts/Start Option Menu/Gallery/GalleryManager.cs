using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public GameObject[] illustPanel;

    public GameObject[] EndingCards;
    public GameObject[] EventCards;

    string EndingKey = "EndingNum";
    string EventKey = "EventNum";
    string serializeText, LoadDataText;

    public static List<int> EndingIndex = new List<int>();
    public List<int> EventIndex = new List<int>();
    public List<int> savedInfo = new List<int>();
    List<int> dummyData;

    bool endingFirst = false;
    bool eventFirst = false;
    int j = 1;

    public string Serialize(List<int> _indexData)
    {
        serializeText = "";
        if (_indexData.Count > 0)
        {
            serializeText += _indexData[0].ToString();
            do
            {
                serializeText += "," + _indexData[j].ToString();
                ++j;
            } while (j < _indexData.Count);
            j = 1;
            return serializeText;
        }
        else
            return null;
    }

    public List<int> Deserialize(string data)
    {
        dummyData = new List<int>();
        string[] parts = data.Split(',');
        for (int i = 0; i < parts.Length; i++)
        {
            dummyData.Add(int.Parse(parts[i]));
        }
        return dummyData;
    }

    private void Start()
    {
        EndingIndex.Add(9);
        EndingIndex.Add(11);
        EndingIndex.Add(2);
        foreach (GameObject i in illustPanel)
        {
            i.SetActive(false);
        }
        string endingSerializedData = Serialize(EndingIndex);
        string eventSerializedData = Serialize(EventIndex);
        PlayerPrefs.SetString(EndingKey, endingSerializedData);
        PlayerPrefs.SetString(EventKey, eventSerializedData);
        PlayerPrefs.Save();
    }

    public void LoadData(int _index)
    {
        if (PlayerPrefs.HasKey(EndingKey))
        {
            LoadDataText = "";
            switch (_index)
            {
                case 0:
                    LoadDataText = PlayerPrefs.GetString("EndingNum");
                    break;

                case 1:
                    LoadDataText = PlayerPrefs.GetString("EventNum");
                    break;

                default:
                    break;
            }
            EndingIndex = new List<int>();
            EndingIndex = Deserialize(LoadDataText);
            showEnding(_index);
        }
    }

    void showEnding(int _index)
    {
        switch (_index)
        {
            case 0:
                if(endingFirst == false)
                {
                    for (int i = 0; i < EndingIndex.Count; i++)
                    {
                        GalleryCards galleryCards = EndingCards[EndingIndex[i]].GetComponent<GalleryCards>();
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
                    for (int i = 0; i < EndingIndex.Count; i++)
                    {
                        GalleryCards galleryCards = EventCards[EndingIndex[i]].GetComponent<GalleryCards>();
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
