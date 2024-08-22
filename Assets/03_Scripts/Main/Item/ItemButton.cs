using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public int buttonItemID;
    public ItemManager itemManager;
    public buttonManager buttonManager;

    int minusMoney = 0;
    int itemOrder;

    public void BuyItem()
    {
        CheckMoney(buttonItemID);
    }

    public void UseItem()
    {
        for (int i = 0; i < itemManager.items.Count; i++)
        {
            int compare = itemManager.items[i].GetComponent<Item>().itemData.ItemID;
            if (compare == buttonItemID)
            {
                itemOrder = i;
                break;
            }

            compare = 0;
        }

        Item itemCs = itemManager.items[itemOrder].GetComponent<Item>();
        itemCs.itemData.ItemCount--;
        DataBase.DB.playerData.itemDatas[itemOrder].ItemCount--;
    }

    public void hideExplainPanel()
    {
        if(transform.name == "useButton")
        {
            if(DataBase.DB.playerData.itemDatas[itemOrder].ItemCount == 0)
                buttonManager.setInvenExplainObj(false);
        }
    }

    void CheckMoney(int _itemID)
    {
        switch (_itemID)
        {
            case 1001:
                minusMoney = 300;
                break;

            case 1002:
                minusMoney = 500;
                break;

            case 1003:
                minusMoney = 700;
                break;

            default:
                break;
        }

        if(DataBase.DB.playerData.money >= minusMoney)
        {
            DataBase.DB.playerData.money -= minusMoney;
            itemManager.checkItem(_itemID);
            minusMoney = 0;
        }
        else
        {
            minusMoney = 0;
        }
    }
}
