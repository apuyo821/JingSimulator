using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryShowFUll : MonoBehaviour
{
    public int endingIndex;
    [SerializeField] Sprite[] illusts;

    [SerializeField] GameObject[] zoomIllustObjs;
    [SerializeField] Image zoomIllust;

    public GameObject activeObj;

    private void Start()
    {
        foreach (GameObject item in zoomIllustObjs)
        {
            item.SetActive(false);
        }
    }

    public void showFull()
    {
        if(activeObj.activeSelf == true)
        {
            foreach (GameObject item in zoomIllustObjs)
            {
                item.SetActive(true);
            }
            zoomIllust.sprite = illusts[endingIndex];
            zoomIllust.SetNativeSize();
        }
    }
    public void BackGallery()
    {
        foreach (GameObject item in zoomIllustObjs)
        {
            item.SetActive(false);
        }
    }
}
