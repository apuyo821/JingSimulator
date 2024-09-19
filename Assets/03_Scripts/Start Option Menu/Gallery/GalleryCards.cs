using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GalleryCards : MonoBehaviour
{
    Image Image;
    public Sprite[] sprites;
    [SerializeField] GameObject hintText;

    public void ImageChange(int _index)
    {
        Image = GetComponent<Image>();
        Image.sprite = sprites[_index];
    }

    public void showHint()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hintText.SetActive(true);
        }
    }
}
