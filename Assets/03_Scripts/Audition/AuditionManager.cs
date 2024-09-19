using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionManager : MonoBehaviour
{
    [SerializeField] GameObject auditionJing;
    [SerializeField] auditionJingAnimControl auditionJingAnimControl;

    [SerializeField] GalleryManager galleryManager;

    public GameObject[] rythmGameSystem;

    public EndingTransition endingTransition;

    int endingType;

    private void Start()
    {
        AudioManager.audioManager.mainAudio.Stop();
        for (int i = 0; i < rythmGameSystem.Length; i++)
        {
            rythmGameSystem[i].SetActive(false);
        }
        
        switch (DataBase.DB.playerData.auditionIndex)
        {
            case 0:
                rythmGameSystem[0].SetActive(true);
                auditionJing.SetActive(true);
                auditionJing.transform.parent = rythmGameSystem[0].transform;
                auditionJing.transform.localPosition = new Vector3(600, 0, 0);
                auditionJing.transform.localScale = new Vector3(90, 90, 90);
                auditionJingAnimControl.firstAuditionAC();
                break;

            case 1:
                rythmGameSystem[1].SetActive(true);
                auditionJing.SetActive(true);
                auditionJing.transform.parent = rythmGameSystem[1].transform;
                auditionJing.transform.localPosition = new Vector3(0, -140, 0);
                auditionJing.transform.localScale = new Vector3(40, 40, 40);
                auditionJingAnimControl.secondAuditionAC();
                break;

            case 2:
                rythmGameSystem[2].SetActive(true);
                break;

            case 3:
                auditionJing.SetActive(false);
                AuditionResultCalculate(DataBase.DB);
                break;

            default:
                break;
        }
    }

    public void AuditionResultCalculate(DataBase dataBase)
    {
        int score = DataBase.DB.playerData.rankScore;
        int first = DataBase.DB.playerData.firstPlace;
        if (score <= 6 && first >= 1)
        {
            //"아이돌";
            endingType = 1;
        }
        else
        {
            if (dataBase.playerData.danceCount >= 40)
            {
                endingType = 2;
                //"댄서";
            }
            else if (dataBase.playerData.vocalCount >= 40)
            {
                endingType = 3;
                //"가수";
            }
            else if (dataBase.playerData.broadcastCount >= 40)
            {
                endingType = 4;
                //"버튜버";
            }
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.GYMCount >= 20)
            {
                endingType = 5;
                //"에어로빅 강사";
            }
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.gameCOunt >= 20)
            {
                endingType = 6;
                //"모션캡쳐";
            }
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.drawingCount >= 20)
            {
                endingType = 7;
                //"발레";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.GYMCount >= 20)
            {
                endingType = 8;
                //"보컬 트레이너";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.drawingCount >= 20)
            {
                endingType = 9;
                //"미술관 큐레이터";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.gameCOunt >= 20)
            {
                endingType = 10;
                //"성우";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.guitarCount >= 20)
            {
                endingType = 11;
                //"밴드";
            }
            else if (dataBase.playerData.broadcastCount >= 20 && dataBase.playerData.GYMCount >= 20)
            {
                endingType = 12;
                //"헬스 유튜버";
            }
            else if (dataBase.playerData.broadcastCount >= 20 && dataBase.playerData.drawingCount >= 20)
            {
                endingType = 13;
                //"밥버거";
            }
            else if (dataBase.playerData.broadcastCount >= 20 && dataBase.playerData.guitarCount >= 20)
            {
                endingType = 14;
                //"기타 유튜버";
            }
            else
            {

            }
        }
        endingType = 13;
        endingTransition.ProcessStart(endingType);
        compareEndingAndSave();
    }

    void compareEndingAndSave()
    {
        bool isExit = false;

        if (DataBase.DB.temporaryEndingData.Count > 0)
        {
            for (int i = 0; i < DataBase.DB.temporaryEndingData.Count; i++)
            {
                if(DataBase.DB.temporaryEndingData[i] == endingType)
                {
                    isExit = true;
                    Debug.Log("존재함");
                    break;
                }
            }
        }
        if (!isExit)
        {
            DataBase.DB.temporaryEndingData.Add(endingType);
        }

        galleryManager.SaveData();
    }
}
