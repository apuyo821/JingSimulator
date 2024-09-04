using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    public GameObject[] UiObjs;

    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
    }
}
