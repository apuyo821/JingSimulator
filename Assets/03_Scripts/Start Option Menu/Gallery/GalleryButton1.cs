using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GalleryButton1 : MonoBehaviour
{
    [SerializeField] GameObject[] buttonImage;  //0:ending Button non, 1:ending Buttons Yes, 2:event Button non, 3:event Button Yes

    private void OnEnable()
    {
        buttonImage[0].SetActive(true);
        buttonImage[1].SetActive(false);
        buttonImage[2].SetActive(true);
        buttonImage[3].SetActive(false);
    }

    public void resizeEndingVer()
    {
        buttonImage[2].SetActive(true);
        buttonImage[3].SetActive(false);
        buttonImage[1].SetActive(true);
        buttonImage[0].SetActive(false);
    }

    public void resizeEventVer()
    {
        buttonImage[2].SetActive(false);
        buttonImage[3].SetActive(true);
        buttonImage[1].SetActive(false);
        buttonImage[0].SetActive(true);
    }
}
