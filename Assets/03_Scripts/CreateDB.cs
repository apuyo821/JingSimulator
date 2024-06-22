using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDB : MonoBehaviour
{
    public GameObject database;

    public void Create()
    {
        Instantiate(database);
    }

    public void DestroyDB()
    {
        Destroy(database);
    }

    /*
    LoadSave는
    게임 진행 도중 다른 세이브로 게임하고 싶을 때

    1, DB가 존재한다면
    1-2, 기존에 있던 DB, Destroy

    2, 새로운 DB 생성

    3, Save에 있는 데이터 옮기는 메소드 실행 
    */
    public void LoadSave()
    {
        if (database.gameObject.activeSelf)
            Destroy(database);
        Instantiate(database);

        //DataBase.DB.LoadData();
    }
}
