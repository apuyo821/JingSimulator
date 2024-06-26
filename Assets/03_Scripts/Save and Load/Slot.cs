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

    //Select 스크립트 불러오기
    private void Start()
    {
        selectCs = selectObj.gameObject.GetComponent<Select>();
    }

    public void LoadData()
    {
        DataBase.DB.LoadData(nowSlot);
    }

    //세이브 슬롯 눌렀을 때, 세이브 파일 유무 판단
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
            selectCs.slotNum = 0;
        }
    }

    //각 슬롯의 인덱스 넘버 넘겨주기
    public void GetNowSlot()
    {
        selectCs.slotNum = nowSlot;
    }
}
