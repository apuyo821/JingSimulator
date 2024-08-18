using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    public int buttonItemID;
    public ItemManager itemManager;

    public void BuyItem()
    {
        itemManager.checkItem(buttonItemID);
    }
}
