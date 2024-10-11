using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testt : MonoBehaviour
{
    public GameObject[] _imgae;
    public Image rawImage;

    public void setNative()
    {
        _imgae[0].SetActive(true);
        _imgae[1].SetActive(true);
        _imgae[2].SetActive(true);
        rawImage.SetNativeSize();
    }
}
