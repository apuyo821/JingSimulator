using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{
    public GameObject[] UiObjs;

    private void Awake()
    {
        Screen.SetResolution(1366, 768, false);
        for (int i = 0; i < UiObjs.Length; i++)
        {
            UiObjs[i].SetActive(false);
        }
    }

    public void DataClear()
    {
        DataBase.DB.playerData = new PlayerData();
    }
}
