using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour
{
    public ScheduleManager scheduleManager;
    public Slider slider;
    public GameObject sliderObj;

    private void Start()
    {
        sliderObj.SetActive(false);
        slider.maxValue = scheduleManager.actFlowTIme;
        slider.minValue = 0;
        slider.value = 0;
    }

    public void processTimerStart()
    {
        slider.maxValue = scheduleManager.actFlowTIme;
        sliderObj.SetActive(true);
        StartCoroutine(processTimer());
    }

    IEnumerator processTimer()
    {
        slider.value = 0;
        while (slider.value != slider.maxValue)
        {
            slider.value += 0.025f;
            yield return new WaitForSeconds(0.025f);
        }
        //yield return new WaitForSeconds(1f);
        slider.value = 0;

        sliderObj.SetActive(false);
        ScheduleManager.isActing = false;

    }
}
