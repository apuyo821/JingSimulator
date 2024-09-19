using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class thirdAuditionManager : MonoBehaviour
{
    public Text rankText;
    public GameObject rankPanel;

    private void OnEnable()
    {
        rankPanel.SetActive(true);
        if (DataBase.DB.playerData.rizz >= 251)
            rankText.text = "1등";
        else if (DataBase.DB.playerData.rizz < 251 && DataBase.DB.playerData.rizz >= 179)
            rankText.text = "2등";
        else if (DataBase.DB.playerData.rizz < 179 && DataBase.DB.playerData.rizz >= 143)
            rankText.text = "3등";
        else if (DataBase.DB.playerData.rizz < 143)
            rankText.text = "4등";
    }

    public void GoMain()
    {
        DataBase.DB.isAuditionEnd = true;
        SceneManager.LoadScene("Main");
    }
}
