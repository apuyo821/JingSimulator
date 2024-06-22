using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class viewNowActing : MonoBehaviour
{
    public Image image;
    public Sprite[] selectImg;
    [SerializeField]
    private int NowIndex;
    int sprNum;
    int nowActing;

    void viewImg()
    {
        nowActing = ScheduleManager.schedules[NowIndex];
        
        switch (nowActing)
        {
            //empty
            case 0:
                sprNum = 0;
                break;

            //workout
            case 11:
                sprNum = 1;
                break;

            //drawing
            case 12:
                sprNum = 2;
                break;

            //vocalTraning
            case 13:
                sprNum = 3;
                break;

            //danceTraning
            case 14:
                sprNum = 4;
                break;

            //actTraining
            case 15:
                sprNum = 5;
                break;

            //skinCare
            case 16:
                sprNum = 6;
                break;

            //미정
            case 17:
                sprNum = 7;
                break;

            //미정
            case 21:
                sprNum = 8;
                break;

            //미정
            case 22:
                sprNum = 9;
                break;

            //미정
            case 23:
                sprNum = 10;
                break;

            //미정
            case 24:
                sprNum = 11;
                break;

            //미정
            case 31:
                sprNum = 12;
                break;

            //미정
            case 32:
                sprNum = 13;
                break;

            //미정
            case 33:
                sprNum = 14;
                break;


            default:
                    break;
        }
        image.sprite = selectImg[sprNum];
    }

    private void Update()
    {
        viewImg();
    }
}
