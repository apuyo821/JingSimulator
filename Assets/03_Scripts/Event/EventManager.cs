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

        switch (DataBase.DB.eventType)
        {
            case 0:
                eventType[0].SetActive(true);
                DataBase.DB.playerData.isGYMEvent = true;
                break;

            case 1:
                eventType[1].SetActive(true);
                DataBase.DB.playerData.isGameEvent = true;
                break;

            case 2:
                eventType[2].SetActive(true);
                DataBase.DB.playerData.isDrawingEvent = true;
                break;

            default:
                SceneManager.LoadScene("Main");
                break;
        }
    }
}
