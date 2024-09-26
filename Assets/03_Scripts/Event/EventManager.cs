using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject[] eventType;

    private void Start()
    {
        foreach (GameObject item in eventType)
        {
            item.SetActive(false);
        }

        if(DataBase.DB.playerData.GYMCount >= 15 && !DataBase.DB.playerData.isGYMEvent)
        {
            eventType[0].SetActive(true);
            DataBase.DB.playerData.isGYMEvent = true;
        }
        else if(DataBase.DB.playerData.gameCOunt >= 15 && !DataBase.DB.playerData.isGameEvent)
        {
            eventType[1].SetActive(true);
            DataBase.DB.playerData.isGameEvent = true;
        }
        else if (DataBase.DB.playerData.drawingCount >= 15 && !DataBase.DB.playerData.isDrawingEvent)
        {
            eventType[2].SetActive(true);
            DataBase.DB.playerData.isDrawingEvent = true;
        }
    }
}
