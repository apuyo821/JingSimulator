using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform invenTransform;

    Item itemCs;
    public List<GameObject> items = new List<GameObject>();

    public ItemData dummyItemData;
    string dummyItemName;
    int itemOrder;
    bool isExit = false;

    public Text moneyTxt;

    public List<Info> itemInfoList;

    bool isListExit;

    private void Awake()
    {
        
    }

    private void Start()
    {
        items = new List<GameObject>();

        LoadData();
    }

    private void Update()
    {
        moneyTxt.text = "\\ " + DataBase.DB.playerData.money.ToString();
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
            DataBase.DB.playerData.itemDatas[itemOrder].ItemCount++;
            isExit = false;
        }
        else
        {
            GetItem(_itemID);
        }
    }

    public void GetItem(int _itemID)
    {
        GameObject itemPrefab = Instantiate(prefab, invenTransform);
        items.Add(itemPrefab);
        itemPrefab.transform.tag = "Item";
        itemCs = itemPrefab.GetComponent<Item>();
        itemCs.itemData.itemName = SetName(_itemID);
        itemCs.itemData.ItemID = _itemID;
        itemCs.itemData.ItemCount++;

        dummyItemData.itemName = SetName(_itemID);
        dummyItemData.ItemCount++;
        dummyItemData.ItemID = _itemID;

        DataBase.DB.playerData.itemDatas.Add(dummyItemData);

        dummyItemData = new ItemData();

        isExit = false;
    }

    public string SetName(int _itemID)
    {
        switch (_itemID)
        {
            case 1001:
                dummyItemName = "burger";
                break;

            case 1002:
                dummyItemName = "jingsix";
                break;

            case 1003:
                dummyItemName = "jellyBin";
                break;

            case 1004:
                dummyItemName = "omurice";
                break;

            default:
                break;
        }
        return dummyItemName;
    }

    public void LoadData()
    {
        if (DataBase.DB.playerData.itemDatas != null)
        {
            for (int i = 0; i < DataBase.DB.playerData.itemDatas.Count; i++)
            {
                GameObject itemPrefab = Instantiate(prefab, invenTransform);
                items.Add(itemPrefab);
                itemPrefab.transform.tag = "Item";
                itemCs = itemPrefab.GetComponent<Item>();
                itemCs.itemData.itemName = DataBase.DB.playerData.itemDatas[i].itemName;
                itemCs.itemData.ItemID = DataBase.DB.playerData.itemDatas[i].ItemID;
                itemCs.itemData.ItemCount = DataBase.DB.playerData.itemDatas[i].ItemCount;
            }
        }
    }
}
