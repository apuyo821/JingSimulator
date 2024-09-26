using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class thirdAuditionManager : MonoBehaviour
{
    public Text rankText;
    public GameObject rankPanel;

    [SerializeField] AuditionManager auditionManager;

    private void OnEnable()
    {
        rankPanel.SetActive(true);
        if (DataBase.DB.playerData.rizz + DataBase.DB.playerData.dance + DataBase.DB.playerData.vocal >= 250)
        {
            rankText.text = "합격!";
            DataBase.DB.thirdAudition = true;
        }
        else
        {
            rankText.text = "불합격";
            DataBase.DB.thirdAudition = false;
        }
    }

    public void GoMain()
    {
        DataBase.DB.isAuditionEnd = true;
        //DataBase.DB.playerData.auditionIndex++;
        auditionManager.LastAudition();
    }
}
