using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GalleryCards : MonoBehaviour
{
    public int endingNum;
    public Sprite[] sprites;
    [SerializeField] GameObject hintText;
    public GameObject illust;


    [SerializeField] GalleryShowFUll galleryShow;
    [Header("이벤트 타입")]
    [SerializeField] string eventType = "";

    [SerializeField] GameObject choosePanel;

    public void ImageChange()
    {
        illust.SetActive(true);
    }

    public void showHint()
    {
        if(illust.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                hintText.SetActive(true);
            }
        }
    }

    public void throwEndingNum()
    {
        galleryShow.endingIndex = endingNum;
        galleryShow.activeObj = illust;
    }

    public void showChoosePanel()
    {
        if (illust.activeSelf == true)
            choosePanel.SetActive(true);
    }

    public void eventProcess()
    {
        if(illust.activeSelf == true)
        {
            switch (eventType)
            {
                case "WorkOutEvent":
                    DataBase.DB.eventType = 0;
                    DataBase.DB.playerData.isGYMEvent = true;
                    SceneManager.LoadScene("EventForGallery");
                    break;

                case "DrawingEvent":
                    DataBase.DB.eventType = 2;
                    DataBase.DB.playerData.isDrawingEvent = true;
                    SceneManager.LoadScene("EventForGallery");
                    break;

                case "GameEvent":
                    DataBase.DB.eventType = 1;
                    DataBase.DB.playerData.isGameEvent = true;
                    SceneManager.LoadScene("EventForGallery");
                    break;

                case "FirstAuditionHard":
                    DataBase.DB.playerData.auditionIndex = 0;
                    DataBase.DB.playerData.vocal = 0;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "FirstAuditionNoraml":
                    DataBase.DB.playerData.auditionIndex = 0;
                    DataBase.DB.playerData.vocal = 30;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "FirstAuditionEasy":
                    DataBase.DB.playerData.auditionIndex = 0;
                    DataBase.DB.playerData.vocal = 50;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "SecondAuditionHard":
                    DataBase.DB.playerData.auditionIndex = 1;
                    DataBase.DB.playerData.dance = 0;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "SecondAuditionNoraml":
                    DataBase.DB.playerData.auditionIndex = 1;
                    DataBase.DB.playerData.dance = 35;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "SecondAuditionEasy":
                    DataBase.DB.playerData.auditionIndex = 1;
                    DataBase.DB.playerData.dance = 222;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "ThirdAuditionPass":
                    DataBase.DB.playerData.auditionIndex = 2;
                    DataBase.DB.playerData.rizz = 300;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                case "ThirdAuditionFail":
                    DataBase.DB.playerData.auditionIndex = 2;
                    DataBase.DB.playerData.dance = 0;
                    DataBase.DB.playerData.vocal = 0;
                    DataBase.DB.playerData.rizz = 0;
                    SceneManager.LoadScene("AuditionForGallery");
                    break;

                default:
                    break;
            }
        }
    }
}
