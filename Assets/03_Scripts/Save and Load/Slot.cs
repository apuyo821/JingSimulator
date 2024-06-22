using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static Slot slot;
    public int nowSlot;
    public GameObject selectObj;
    public GameObject warningPanel;

    Select selectCs;

    private void Start()
    {
        selectCs = selectObj.gameObject.GetComponent<Select>();
    }

    public void LoadData()
    {
        DataBase.DB.LoadData(nowSlot);
    }

    public void checkSaveExist()
    {
        if (selectCs.saveFile[nowSlot-1])
        {
            //세이브 덮어 씌워도 되냐는 문구 띄우기
            warningPanel.SetActive(true);
        }
        else
        {
            //세이브 없으면 바로 세이브
            DataBase.DB.SaveData(nowSlot);
        }
    }

    public void SaveData()
    {
        DataBase.DB.SaveData(nowSlot);
        if (warningPanel.activeSelf)
            warningPanel.SetActive(false);
    }

    public void GetNowSlot()
    {
        selectCs.slotNum = nowSlot;
    }
}
