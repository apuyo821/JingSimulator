using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GalleryPage : MonoBehaviour
{
    [SerializeField] GameObject[] pageObj;

    [Space(10f)]
    [SerializeField] Button[] pageButtons;    //0:Left, 1:Right
    [SerializeField] int pageNum = 0;
    [SerializeField] TMP_Text pageNumTxt;

    private void OnDisable()
    {
        pageNum = 0;
    }

    private void OnEnable()
    {
        pageNum = 0;
        for (int i = 0; i < pageObj.Length; i++)
        {
            pageObj[i].SetActive(false);
        }
        pageCheck();
    }

    private void Update()
    {
        pageCheck();
        pageNumTxt.text = (pageNum + 1).ToString();
    }

    public void LeftPage()
    {
        pageNum--;
        pageCheck();
    }

    public void RightPage()
    {
        pageNum++;
        pageCheck();
    }

    void pageCheck()
    {
        for (int i = 0; i < pageObj.Length; i++)
        {
            pageObj[i].SetActive(false);
        }
        switch (pageNum)
        {
            case 0:
                pageButtons[0].interactable = false;
                pageObj[pageNum].SetActive(true);
                break;

            case 1:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = true;
                pageObj[pageNum].SetActive(true);
                break;

            case 2:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = true;
                pageObj[pageNum].SetActive(true);
                break;

            case 3:
                pageButtons[0].interactable = true;
                pageButtons[1].interactable = false;
                pageObj[pageNum].SetActive(true);
                break;

            default:
                break;
        }
    }

}
