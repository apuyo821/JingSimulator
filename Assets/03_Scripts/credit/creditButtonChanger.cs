using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditButtonChanger : MonoBehaviour
{
    public GameObject creditBtn;

    string creditKey = "gameClearKey";

    private void Start()
    {
        loadGameClear();
        check();
    }

    public void saveGameClear()
    {
        PlayerPrefs.SetInt(creditKey, DataBase.DB.gameClear);
        PlayerPrefs.Save();
    }

    public void loadGameClear()
    {
        if (PlayerPrefs.HasKey(creditKey))
            DataBase.DB.gameClear = PlayerPrefs.GetInt("gameClearKey");
    }

    public void check()
    {
        if(DataBase.DB.gameClear == 1)
        {
            creditBtn.SetActive(true);
        }
        else if(DataBase.DB.gameClear == 0)
        {
            creditBtn.SetActive(false);
        }
    }
}
