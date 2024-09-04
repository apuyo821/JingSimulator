using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public int ItemID;
    public int ItemCount;
}

public class Item : MonoBehaviour
{
    public Text CountTxt;

    public Sprite[] itemSprite;

    public ItemData itemData;

    public GameObject buyButton;
    ItemButton buyButtonCs;
    ItemButton useButtonCs;
    public Image itemImage;

    ItemManager ItemManager;
    GameObject[] explainObj = new GameObject[3];

    private void Start()
    {
        if(transform.tag == "Shop")
            buyButtonCs = buyButton.GetComponent<ItemButton>();
        if(transform.tag == "Item")
        {
            ItemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        }

        itemImage = transform.GetChild(0).GetComponent<Image>();
        Invoke("ImageSet", 0.001f);
    }

    public void ThrowItemID()
    {
        buyButtonCs.buttonItemID = itemData.ItemID;
    }

    private void Update()
    {            
        if(transform.tag == "Item")
            CountTxt.text = itemData.ItemCount.ToString();

        if(itemData.ItemCount == 0)
        {
            gameObject.SetActive(false);
            ItemManager.items.Remove(this.gameObject);
            for (int i = 0; i < DataBase.DB.playerData.itemDatas.Count; i++)
            {
                if(itemData.ItemID == DataBase.DB.playerData.itemDatas[i].ItemID)
                {
                    DataBase.DB.playerData.itemDatas.Remove(DataBase.DB.playerData.itemDatas[i]);
                }
            }
        }
    }

    void ImageSet()
    {
        int sprNum = itemData.ItemID - 1001;
        itemImage.sprite = itemSprite[sprNum];
    }

    public void GetExplainObj()
    {
        if(transform.tag == "Item")
        {
            GameObject explainPanel = GameObject.Find("panel_Explain_decide");
            //0 = Item Icon  1 = explain Text    2 = use Button
            for (int i = 0; i < 3; i++)
            {
                explainObj[i] = explainPanel.transform.GetChild(i).gameObject;
                explainObj[i].SetActive(true);
            }
            setUI(explainObj[0], explainObj[1], explainObj[2]);
        }
    }

    void setUI(GameObject _imageObj, GameObject _textObj, GameObject _buttonObj)
    {
        Image itemIcon = _imageObj.transform.GetChild(0).GetComponent<Image>();
        Text explainTxt = _textObj.GetComponentInChildren<Text>();
        useButtonCs = _buttonObj.GetComponent<ItemButton>();
        useButtonCs.buttonItemID = itemData.ItemID;
        switch (itemData.ItemID)
        {
            case 1001:
                itemIcon.sprite = itemSprite[0];
                explainTxt.text = "닭가슴살 햄버거 : 체력을 회복해줍니다";
                break;

            case 1002:
                itemIcon.sprite = itemSprite[1];
                explainTxt.text = "징누 : 정신력을 회복해줍니다.";
                break;

            case 1003:
                itemIcon.sprite = itemSprite[2];
                explainTxt.text = "젤리 빈 : 랜덤으로 스탯을 증감소합니다.";
                break;

            case 1004:
                itemIcon.sprite = itemSprite[3];
                explainTxt.text = "오무라이스 : 오무라이스.";
                break;

            default:
                break;
        }
    }
}
