using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1366, 768, false);
    }

    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
    }
}
