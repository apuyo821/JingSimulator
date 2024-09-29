using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryEventManager : MonoBehaviour
{
    [SerializeField] GameObject[] eventCs;

    private void Start()
    {
        foreach (GameObject item in eventCs)
        {
            item.SetActive(false);
        }

        if(DataBase.DB.playerData.isDrawingEvent == true)
        {
            eventCs[0].SetActive(true);
            DataBase.DB.playerData.isDrawingEvent = false;
        }
        else if(DataBase.DB.playerData.isGameEvent == true)
        {
            eventCs[1].SetActive(true);
            DataBase.DB.playerData.isGameEvent = false;
        }
        else if(DataBase.DB.playerData.isGYMEvent == true)
        {
            eventCs[2].SetActive(true);
            DataBase.DB.playerData.isGYMEvent = false;
        }
    }

}
