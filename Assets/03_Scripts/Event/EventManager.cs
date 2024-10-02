using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField] GameObject[] eventType;
    [SerializeField] GalleryManager galleryManager;

    [SerializeField] GameObject drawingResetButton;
    int eventTypeNum;

    private void Awake()
    {
        Invoke("framelimit", 1f);
        drawingResetButton.SetActive(false);
    }

    void framelimit()
    {
        Application.targetFrameRate = 60;
    }


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
                eventTypeNum = 2;
                break;

            case 1:
                eventType[1].SetActive(true);
                DataBase.DB.playerData.isGameEvent = true;
                eventTypeNum = 1;
                break;

            case 2:
                eventType[2].SetActive(true);
                DataBase.DB.playerData.isDrawingEvent = true;
                drawingResetButton.SetActive(true);
                eventTypeNum = 3;
                break;

            default:
                SceneManager.LoadScene("Main");
                break;
        }

        compareEventAndSave();
    }

    void compareEventAndSave()
    {
        bool isExit = false;

        if (DataBase.DB.temporaryEventData.Count > 0)
        {
            for (int i = 0; i < DataBase.DB.temporaryEventData.Count; i++)
            {
                if (DataBase.DB.temporaryEventData[i] == eventTypeNum)
                {
                    isExit = true;
                    Debug.Log("Á¸ÀçÇÔ");
                    break;
                }
            }
        }
        if (!isExit)
        {
            DataBase.DB.temporaryEventData.Add(eventTypeNum);
        }

        galleryManager.SaveData();
    }
}
