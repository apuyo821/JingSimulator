using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    private void Awake()
    {
        Invoke("framelimit", 1f);
    }

    void framelimit()
    {
        Application.targetFrameRate = 60;
    }

    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
        DataBase.DB.playerData.itemDatas = new List<ItemData>();
    }
}
