using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class viewNowActing : MonoBehaviour
{
    public Image image;
    public Sprite[] selectImg;

    [SerializeField] int NowIndex;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = selectImg[ScheduleManager.schedules[NowIndex]];
    }
}
