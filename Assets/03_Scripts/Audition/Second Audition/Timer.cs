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
        isEventing = true;
    }

    public void timeStop()
    {
        
        StopCoroutine(timeProcessCoroutine);
        isEventing = false;
        slider.value = 0;
    }

    IEnumerator timerProcess()
    {
        while (slider.value != time)
        {
            slider.value++;
            yield return new WaitForSeconds(1f);
        }
        if(slider.value == time)
        {
            noteObj = GameObject.FindGameObjectWithTag("Note");
            noteCS = noteObj.GetComponent<Notes>();
            noteCS.RemoveProcess();
            RGManager.RGinstance.winOrLose(1);  //fail
            yield return null;
            slider.value = 0;
        }
    }
}
