using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class shopItem : MonoBehaviour
{
    [SerializeField] ItemManager itemManager;
    [SerializeField] GameObject buyButton;
    [SerializeField] Image buttonImage;
    [SerializeField] Button buyButtonButton;
    [SerializeField] int itemID;
    [SerializeField] GameObject detailInfoPanel;
    [SerializeField] TMP_Text tmpText;

    [SerializeField] AudioSource buyAudio;
    int minusMoney = 0;

    private void OnEnable()
    {
        detailInfoPanel.SetActive(false);
        buttonImage.color = new Color(255, 255, 255, 1);
        buyButtonButton.enabled = true;
    }

    public void BuyItem()
    {
        CheckMoney(itemID);
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
                tmpText.text = "미숙함을 20 줄여줍니다.\n매력이 10 올라갑니다";
                break;

            default:
                break;
        }
    }

    public void pointerExit()
    {
        detailInfoPanel.SetActive(false);
    }

    void CheckMoney(int _itemID)
    {
        switch (_itemID)
        {
            case 1001:
                minusMoney = 140;
                break;

            case 1002:
                minusMoney = 80;
                break;

            case 1003:
                minusMoney = 200;
                break;

            case 1004:
                minusMoney = 300;
                break;

            default:
                break;
        }

        if (DataBase.DB.playerData.money >= minusMoney)
        {
            DataBase.DB.playerData.money -= minusMoney;
            itemManager.checkItem(_itemID);
            AudioManager.audioManager.sfx[0].Play();
            minusMoney = 0;
        }
        else
        {
            StartCoroutine(sparkleButton());
            minusMoney = 0;
            AudioManager.audioManager.sfx[5].Play();
        }

        IEnumerator sparkleButton()
        {
            //buyButtonButton.interactable = false;
            buyButtonButton.enabled = false;
            Color previusColor = buttonImage.color;
            Color newColor = new Color(255, 0, 0);
            buttonImage.color = newColor;
            yield return new WaitForSeconds(0.5f);
            buttonImage.color = previusColor;
            buyButtonButton.enabled = true;
        }
    }
}
