using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform transform;

    public Item itemCs;
    List<int> itemDB = new List<int>();
    public List<GameObject> items = new List<GameObject>();

    int itemOrder;
    bool isExit = false;

    public void checkItem(int _itemID)
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            Debug.Log(itemDB[i]);
            if (itemDB[i] == _itemID)
            {
                isExit = true;
                itemOrder = i;
                break;
            }
            else
                isExit = false;
        }

        if (isExit)
        {
            items[itemOrder].GetComponent<Item>().itemData.ItemCount++;
        }
        else
        {
            GetItem(_itemID);
        }
    }

    public void GetItem(int _itemID)
    {
        itemDB.Add(_itemID);
        GameObject itemPrefab = Instantiate(prefab, transform);
        items.Add(itemPrefab);
        itemCs = itemPrefab.GetComponent<Item>();
        itemCs.itemData.ItemID = _itemID;
        itemCs.itemData.ItemCount++;
    }
}
