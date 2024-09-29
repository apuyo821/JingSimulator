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
    [Space(5)]
    [Header("엔딩 멘트 관련")]
    [SerializeField] GameObject itrObj;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;
    public static bool isEndingMenting = false;
    int dialoguLength;
    [SerializeField] GameObject[] endingMentObjs;
    [SerializeField] GameObject backSuEnding;

    private void Start()
    {
        for (int i = 0; i < rythmGameSystem.Length; i++)
        {
            rythmGameSystem[i].SetActive(false);
        }

        for (int i = 0; i < endingMentObjs.Length; i++)
        {
            endingMentObjs[i].SetActive(false);
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
                //auditionJingAnimControl.secondAuditionAC();
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

        DataBase.DB.playerData.dDay--;
    }

    public void LastAudition()
    {
        auditionJing.SetActive(false);
        AuditionResultCalculate(DataBase.DB);
    }

    public void AuditionResultCalculate(DataBase dataBase)
    {
        if (dataBase.thirdAudition == true)
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
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.GYMCount >= 10)
            {
                endingType = 5;
                //"에어로빅 강사";
            }
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.gameCOunt >= 10)
            {
                endingType = 6;
                //"모션캡쳐";
            }
            else if (dataBase.playerData.danceCount >= 20 && dataBase.playerData.drawingCount >= 10)
            {
                endingType = 7;
                //"발레";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.GYMCount >= 10)
            {
                endingType = 8;
                //"보컬 트레이너";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.drawingCount >= 10)
            {
                endingType = 9;
                //"미술관 큐레이터";
            }
            else if (dataBase.playerData.vocalCount >= 20 && dataBase.playerData.gameCOunt >= 10)
            {
                endingType = 10;
                //"성우";
            }
            else if (dataBase.playerData.broadcastCount >= 20 && dataBase.playerData.GYMCount >= 10)
            {
                endingType = 11;
                //"헬스 유튜버";
            }
            else if (dataBase.playerData.broadcastCount >= 20 && dataBase.playerData.drawingCount >= 10)
            {
                endingType = 12;
                //"밥버거";
            }
            else
            {

            }
        }
        Debug.Log(endingType);
        if(endingType != 0)
        {
            StartCoroutine(endingMent(endingType));
            compareEndingAndSave();
        }
        else
        {
            backSuEnding.SetActive(true);
        }
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

    IEnumerator endingMent(int _endingType)
    {
        float lineX = 0, lineY = 0, _dialoguLength = 0;
        string dialogueName = "";

        switch (_endingType)
        {
            case 1:
                lineX = 37;
                lineY = 37;
                _dialoguLength = 1;
                dialogueName = "아이돌 엔딩";
                break;

            case 2:
                lineX = 45;
                lineY = 46;
                _dialoguLength = 2;
                dialogueName = "댄서 엔딩";
                break;

            case 3:
                lineX = 39;
                lineY = 40;
                _dialoguLength = 2;
                dialogueName = "가수 엔딩";
                break;

            case 4:
                lineX = 53;
                lineY = 54;
                _dialoguLength = 2;
                dialogueName = "버튜버 엔딩";
                break;

            case 5:
                lineX = 47;
                lineY = 48;
                _dialoguLength = 2;
                dialogueName = "에어로빅 강사 엔딩";
                break;

            case 6:
                lineX = 51;
                lineY = 52;
                _dialoguLength = 2;
                dialogueName = "모션캡쳐 엔딩";
                break;

            case 7:
                lineX = 49;
                lineY = 50;
                _dialoguLength = 2;
                dialogueName = "발레 엔딩";
                break;

            case 8:
                lineX = 41;
                lineY = 42;
                _dialoguLength = 2;
                dialogueName = "보컬 트레이너 엔딩";
                break;

            case 9:
                lineX = 43;
                lineY = 44;
                _dialoguLength = 2;
                dialogueName = "미술관 큐레이터 엔딩";
                break;

            case 10:
                lineX = 59;
                lineY = 60;
                _dialoguLength = 2;
                dialogueName = "성우 엔딩";
                break;

            case 11:
                lineX = 55;
                lineY = 56;
                _dialoguLength = 2;
                dialogueName = "헬스 유튜버 엔딩";
                break;

            case 12:
                lineX = 57;
                lineY = 58;
                _dialoguLength = 2;
                dialogueName = "밥버거 엔딩";
                break;

            default:
                break;
        }

        isEndingMenting = true;
        itrCs.dialogueEvent.line.x = lineX;
        itrCs.dialogueEvent.line.y = lineY;
        dialoguLength = (int)_dialoguLength;
        itrCs.dialogueEvent.name = dialogueName;
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());

        yield return new WaitUntil(() => isEndingMenting == false);
        endingTransition.ProcessStart(_endingType);
    }
}
