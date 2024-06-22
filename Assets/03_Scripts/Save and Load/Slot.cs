using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static Slot slot;

    public int nowSlot;

    public void LoadData()
    {
        DataBase.DB.LoadData(nowSlot);
    }

    public void SaveData()
    {
        DataBase.DB.SaveData(nowSlot);
    }
}
