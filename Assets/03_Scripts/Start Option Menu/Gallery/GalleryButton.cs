using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryButton : MonoBehaviour
{
    [SerializeField] Image[] buttonImage;
    [SerializeField] Sprite[] sprites;  //0:엔딩 온, 1:엔딩 오프, 2:이벤트 온, 3:이벤트 오프

    private void OnEnable()
    {
        buttonImage[0].sprite = sprites[1];
        buttonImage[1].sprite = sprites[3];
    }

    public void clickEnding()
    {
        buttonImage[0].sprite = sprites[0];
        buttonImage[1].sprite = sprites[3];
    }

    public void clickEvent()
    {
        buttonImage[0].sprite = sprites[1];
        buttonImage[1].sprite = sprites[2];
    }
        
}
