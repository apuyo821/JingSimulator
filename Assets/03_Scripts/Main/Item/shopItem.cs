using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shopItem : MonoBehaviour
{
    public int itemID;
    public GameObject detailInfoPanel;
    public TMP_Text tmpText;
    

    private void Start()
    {
        detailInfoPanel.SetActive(false);
    }

    public void showDetailInfo()
    {
        detailInfoPanel.SetActive(true);
        switch (itemID)
        {
            case 1001:
                tmpText.text = "체력을 8만큼 회복시킵니다.";
                break;

            case 1002:
                tmpText.text = "정신력을 6만큼 회복시킵니다.";
                break;

            case 1003:
                tmpText.text = "스탯을 랜덤하게 올리거나 내립니다. \n증가(1~5), 감소(1~3), 매력(1~3) 증가\n*매력은 예외";
                break;

            case 1004:
                tmpText.text = "오무라이스";
                break;

            default:
                break;
        }
    }

    public void pointerExit()
    {
        detailInfoPanel.SetActive(false);
    }
}
