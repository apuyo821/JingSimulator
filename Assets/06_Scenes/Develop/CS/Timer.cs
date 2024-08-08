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

    public IEnumerator timeProcessCoroutine;

    public bool isEventing;

    private void Start()
    {
        slider.value = 0;
        slider.maxValue = time;
        timeProcessCoroutine = timerProcess();    
    }

    public void timerStart()
    {
        StartCoroutine(timeProcessCoroutine);
    }

    public void timeStop()
    {
        
        StopCoroutine(timeProcessCoroutine);
        slider.value = 0;
    }

    IEnumerator timerProcess()
    {
        isEventing = true;
        while (isEventing)
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
        RGManager.RGinstance.Fail();
        yield return null;
        isEventing = false;
        slider.value = 0;
    }
}
