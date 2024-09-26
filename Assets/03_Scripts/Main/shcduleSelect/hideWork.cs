using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideWork : MonoBehaviour
{
    [SerializeField] GameObject hamDragObj;

    private void OnEnable()
    {
        if (DataBase.DB.playerData.dDay == 40 || DataBase.DB.playerData.dDay == 39)
        {
            hamDragObj.SetActive(false);
        }
        else
            hamDragObj.SetActive(true);
    }

}
