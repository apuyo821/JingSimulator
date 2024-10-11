using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideWork : MonoBehaviour
{
    [SerializeField] GameObject hamDragObj;
    [SerializeField] GameObject text;

    private void OnEnable()
    {
        if (DataBase.DB.playerData.dDay == 40 || DataBase.DB.playerData.dDay == 39)
        {
            hamDragObj.SetActive(false);
            text.SetActive(false);
        }
        else
        {
            hamDragObj.SetActive(true);
            text.SetActive(true);
        }
    }

}
