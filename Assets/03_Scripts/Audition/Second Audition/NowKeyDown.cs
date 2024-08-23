using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowKeyDown : MonoBehaviour
{
    public Image image;
    public Sprite defaultImage;
    public Sprite pressdInmage;

    public KeyCode keyToPress;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            image.sprite = pressdInmage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            image.sprite = defaultImage;
        }

    }
}
