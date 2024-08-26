using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEManager : MonoBehaviour
{
    private void Start()
    {
        DESetting();
    }

    void DESetting()
    {
        DEJudge.isEnter = false;
        DENote.isClicked = false;
        DENote.deNoteIndex = 0;
    }
}
