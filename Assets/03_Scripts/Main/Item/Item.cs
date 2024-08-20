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
    BuyButton buyButtonCs;
    public Image itemImage;

    private void Start()
    {
        if(transform.tag == "Shop")
            buyButtonCs = buyButton.GetComponent<BuyButton>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        Invoke("ImageSet", 0.1f);
    }

    public void ThrowItemID()
    {
        buyButtonCs.buttonItemID = itemData.ItemID;
    }

    private void Update()
    {            
        CountTxt.text = itemData.ItemCount.ToString();

        if(itemData.ItemCount == 0)
        {
            CountTxt.gameObject.SetActive(false);
        }
        else
        {
            CountTxt.gameObject.SetActive(true);
        }
    }

    void ImageSet()
    {
        int sprNum = itemData.ItemID - 1001;
        itemImage.sprite = itemSprite[sprNum];
    }
}
