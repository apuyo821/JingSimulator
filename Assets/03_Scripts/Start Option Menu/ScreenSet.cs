using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
        DataBase.DB.playerData.itemDatas = new List<ItemData>();
    }
}
