using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryPage : MonoBehaviour
{
    [Header("엔딩")]
    [SerializeField] GameObject[] endingPageObj;
    [Space(10)]
    [Header("이벤트")]
    [SerializeField] GameObject[] eventPageObj;

    [Space(10f)]
    [SerializeField] Button[] pageButtons;    //0:Left, 1:Right
    [SerializeField] int pageNum = 0;


    private void OnEnable()
    {
        pageNum = 0;
        if (transform.gameObject.name == "Ending Illust Panel")
        {
            for (int i = 0; i < endingPageObj.Length; i++)
            {
                endingPageObj[i].SetActive(false);
            }
            EndingPageCheck();
        }
        else if(transform.gameObject.name == "Event Illust  Panel")
        {
            for (int i = 0; i < endingPageObj.Length; i++)
            {
                endingPageObj[i].SetActive(false);
            }
            EventPageCheck();
        }
        
    }

    private void Update()
    {
        if (transform.gameObject.name == "Ending Illust Panel")
            EndingPageCheck();
        else if (transform.gameObject.name == "Event Illust  Panel")
            EventPageCheck();
    }

    public void EndingLeftPage()
    {
        pageNum--;
        EndingPageCheck();
    }

    public void EndingRightPage()
    {
        pageNum++;
        EndingPageCheck();
    }

    public void EventLeftPage()
    {
        pageNum--;
        EventPageCheck();
    }

    public void EventRightPage()
    {
        pageNum++;
        EventPageCheck();
    }

    void EndingPageCheck()
    {
        for (int i = 0; i < endingPageObj.Length; i++)
        {
            endingPageObj[i].SetActive(false);
        }
        switch (pageNum)
        {
            case 0:
                pageButtons[0].interactable = false;
                pageButtons[1].interactable = true;
                endingPageObj[pageNum].SetActive(true);
                break;

            case 1:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = true;
                endingPageObj[pageNum].SetActive(true);
                break;

            case 2:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = false;
                endingPageObj[pageNum].SetActive(true);
                break;

            default:
                break;
        }
    }

    void EventPageCheck()
    {
        for (int i = 0; i < eventPageObj.Length; i++)
        {
            eventPageObj[i].SetActive(false);
        }
        switch (pageNum)
        {
            case 0:
                pageButtons[0].interactable = false;
                pageButtons[1].interactable = true;
                eventPageObj[pageNum].SetActive(true);
                break;

            case 1:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = true;
                eventPageObj[pageNum].SetActive(true);
                break;

            case 2:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = true;
                eventPageObj[pageNum].SetActive(true);
                break;

            case 3:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = false;
                eventPageObj[pageNum].SetActive(true);
                break;

            default:
                break;
        }
    }

}
