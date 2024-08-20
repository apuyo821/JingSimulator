using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ItemDataTest
{
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
    public ItemData[] itemDatas;
}

public class ItemManager : MonoBehaviour
{
    public ItemDataTest itemDataTests;

    public GameObject prefab;
    public Transform transform;

    Item itemCs;
    public List<GameObject> items = new List<GameObject>();

    string dummyItemName;
    int itemOrder;
    bool isExit = false;

    string path;

    private void Start()
    {
        itemDataTests.itemDatas = new ItemData[3];

        path = Application.persistentDataPath + "/TestSave";
    }

    public void checkItem(int _itemID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            int compare = items[i].GetComponent<Item>().itemData.ItemID;
            if (compare == _itemID)
            {
                isExit = true;
                itemOrder = i;
                break;
            }
            else
                isExit = false;

            compare = 0;
        }


        if (isExit)
        {
            items[itemOrder].GetComponent<Item>().itemData.ItemCount++;
            itemDataTests.itemDatas[_itemID - 1001].ItemCount++;
        }
        else
        {
            GetItem(_itemID);
        }
    }

    public void GetItem(int _itemID)
    {
        GameObject itemPrefab = Instantiate(prefab, transform);
        items.Add(itemPrefab);
        itemCs = itemPrefab.GetComponent<Item>();
        itemCs.itemData.itemName = SetName(_itemID);
        itemCs.itemData.ItemID = _itemID;
        itemCs.itemData.ItemCount++;

        itemDataTests.itemDatas[_itemID - 1001].ItemCount++;
        itemDataTests.itemDatas[_itemID - 1001].ItemID = _itemID;
        itemDataTests.itemDatas[_itemID - 1001].itemName = SetName(_itemID);
}

    public string SetName(int _itemID)
    {
        switch (_itemID)
        {
            case 1001:
                dummyItemName = "하트";
                break;

            case 1002:
                dummyItemName = "Ine";
                break;

            case 1003:
                dummyItemName = "dumbell";
                break;

            default:
                break;
        }
        return dummyItemName;
    }

    public void SaveDataTest()
    {
        string JsonData = JsonUtility.ToJson(itemDataTests, true);
        File.WriteAllText(path, JsonData);
    }

    public void LoadDataTest()
    {
        string JsonData = File.ReadAllText(path);
        itemDataTests = JsonUtility.FromJson<ItemDataTest>(JsonData);
    }
}
