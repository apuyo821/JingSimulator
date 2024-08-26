using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DERing : MonoBehaviour
{
    bool isEnd = true;

    public float time, velo, minusTime;
    Coroutine runningCoroutine = null;

    private void Start()
    {
        runningCoroutine = StartCoroutine(small());
        StartCoroutine(small());
        StartCoroutine(timer());
    }

    public void againSmall()
    {
        StopCoroutine(runningCoroutine);
        StartCoroutine(small());
    }

    IEnumerator small()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);
        while (transform.localScale.x >= 0.05)
        {
            transform.localScale = new Vector3(transform.localScale.x - velo, transform.localScale.y - velo);
            yield return new WaitForSeconds(minusTime);
        }
        isEnd = false;
        if (!DENote.isClicked)
            StartCoroutine(small());
    }

    IEnumerator timer()
    {
        while (isEnd)
        {
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }
    }
}
