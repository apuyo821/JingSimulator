using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEJudge : MonoBehaviour
{
    public static bool isEnter = false;

    private void Start()
    {
        isEnter = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Note")
        {
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnter = false;
    }
}
