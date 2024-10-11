using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DENote : MonoBehaviour
{
    public GameObject land;
    public GameObject[] DENotes;

    public static bool isClicked = false;
    public static int deNoteIndex = 0;
    public bool isMouseOnNote = false;

    DERing dERing;
    [SerializeField] DEManager deManager;

    private void Start()
    {
        dERing = transform.GetChild(0).gameObject.GetComponent<DERing>();
        land.SetActive(false);
    }

    public void ShowLand()
    {
        if (Input.GetMouseButtonDown(0))
        {
            land.SetActive(true);
        }
    }

    private void Update()
    {
        if(isMouseOnNote)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (DEJudge.isEnter)
                {
                    isClicked = true;
                    land.SetActive(true);
                    DENotes[deNoteIndex].SetActive(false);
                    deNoteIndex++;
                    if(deNoteIndex != 3)
                        DENotes[deNoteIndex].SetActive(true);
                    isClicked = false;
                    deManager.animSet();
                }
                else
                {
                    dERing.againSmall();
                }
            }
            
        }
    }

    private void OnMouseEnter()
    {
        isMouseOnNote = true;
    }

    private void OnMouseExit()
    {
        isMouseOnNote = false;
    }
}
