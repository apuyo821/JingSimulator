using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public int buttonItemID;
    public ItemManager itemManager;
    public buttonManager buttonManager;

    public AudioSource omuriceAudioSource;
    public GameObject jellyBinIntroducePanel;
    public GameObject skipButton;

    int minusMoney = 0;
    int itemOrder;

    public GameObject buyButton;
    Image buttonImage;

    private void Start()
    {
        buttonImage = buyButton.GetComponent<Image>();
    }

    public void BuyItem()
    {
        CheckMoney(buttonItemID);
    }

    public void UseItem()
    {
        for (int i = 0; i < itemManager.items.Count; i++)
        {
            int compare = itemManager.items[i].GetComponent<Item>().itemData.ItemID;
            if (compare == buttonItemID)
            {
                itemOrder = i;
                break;
            }

            compare = 0;
        }

        Item itemCs = itemManager.items[itemOrder].GetComponent<Item>();
        itemCs.itemData.ItemCount--;
        DataBase.DB.playerData.itemDatas[itemOrder].ItemCount--;
        switch (buttonItemID)
        {
            case 1001:
                DataBase.DB.playerData.HP += 6;
                break;

            case 1002:
                DataBase.DB.playerData.MP += 4;
                break;

            case 1003:
                int random = Random.Range(1, 5);
                int plusminus = Random.Range(-3, 6);
                string changeString ,statusName ="", pmText ="";
                switch (random)
                {
                    case 0:
                        statusName = "손재주";
                        DataBase.DB.playerData.deft += plusminus;
                        break;

                    case 1:
                        statusName = "가창력";
                        DataBase.DB.playerData.vocal += plusminus;
                        break;

                    case 2:
                        statusName = "근력";
                        DataBase.DB.playerData.strength += plusminus;
                        break;

                    case 3:
                        statusName = "매력";
                        DataBase.DB.playerData.rizz += plusminus;
                        break;

                    case 4:
                        statusName = "댄스";
                        DataBase.DB.playerData.dance += plusminus;
                        break;

                    default:
                        break;
                }
                if (plusminus > 0)
                    pmText = "증가";
                else
                    pmText = "감소";
                TMP_Text introduceText = jellyBinIntroducePanel.gameObject.GetComponentInChildren<TMP_Text>();
                changeString = introduceText.text;
                introduceText.text = changeString.Replace("OO".ToString(), statusName).Replace("n".ToString(),plusminus.ToString()).Replace("MM".ToString(),pmText);
                jellyBinIntroducePanel.SetActive(true);
                break;

            //useButton 클릭하자마자 소리 나오고 스킵 버튼 나오기
            case 1004:
                DataBase.DB.playerData.rizz += 10;
                skipButton.SetActive(true);
                omuriceAudioSource.Play();
                break;

            default:
                break;
        }
    }

    public void omuriceSkip()
    {
        omuriceAudioSource.Stop();
    }

    public void hideExplainPanel()
    {
        if(transform.name == "useButton")
        {
            if(DataBase.DB.playerData.itemDatas[itemOrder].ItemCount == 0)
                buttonManager.setInvenExplainObj(false);
        }
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

        if(DataBase.DB.playerData.money >= minusMoney)
        {
            DataBase.DB.playerData.money -= minusMoney;
            itemManager.checkItem(_itemID);
            minusMoney = 0;
        }
        else
        {
            StartCoroutine(sparkleButton());
            minusMoney = 0;
        }
    }

    IEnumerator sparkleButton()
    {
        Button buyButtonButton = buyButton.GetComponent<Button>();
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
