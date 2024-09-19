using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEManager : MonoBehaviour
{
    [SerializeField] Animator jingAnimator;
    public int noteNum = 0;

    [SerializeField] GameObject[] images;

    private void Start()
    {
        DESetting();
        noteNum = 0;
    }

    void DESetting()
    {
        DEJudge.isEnter = false;
        DENote.isClicked = false;
        DENote.deNoteIndex = 0;
    }

    public void animSet()
    {
        switch (noteNum)
        {
            case 0:
                jingAnimator.SetBool("firstDrawing", true);
                break;

            case 1:
                jingAnimator.SetBool("secondDrawing", true);
                break;

            case 2:
                jingAnimator.SetBool("lastDrawing", true);
                break;

            default:
                break;
        }
        noteNum++;
        if(noteNum == 3)
        {
            Invoke("showImage", 1f);
        }
    }

    void showImage()
    {
        foreach (GameObject i in images)
        {
            i.SetActive(false);
        }
        images[3].SetActive(true);
    }
}
