using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider slider;
    public int time;

    public GameObject noteObj;
    public Notes noteCS;

    private void Start()
    {
        slider.value = 0;
        slider.maxValue = time;
    }

    public void timerStart()
    {
        StartCoroutine(timerProcess());
    }

    IEnumerator timerProcess()
    {
        while (true)
        {
            slider.value++;
            if (slider.value == time)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }
        noteObj = GameObject.FindGameObjectWithTag("Note");
        noteCS = noteObj.GetComponent<Notes>();
        noteCS.RemoveProcess();
        slider.value = 0;
    }
}
