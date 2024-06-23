using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TaskSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DetailInfoPanel;
    public float time = 0;
    [SerializeField] float waitingTime = 1.0f;
    public bool onPointer;

    //포인터가 들어왔을 때, 진행
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Action());
        onPointer = true;
    }
    
    //slot위에 일정 시간 동안 올려놨을 때, 세부사항 띄우기
    IEnumerator Action()
    {
        while (time < waitingTime)
        {
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
            if (onPointer == false)
                break;
        }
        if (time >= waitingTime)
            DetailInfoPanel.SetActive(true);
    }

    //포인터가 벗어 날 때, 초기화
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointer = false;
        time = 0;
        while (DetailInfoPanel.gameObject.activeSelf)
        {
            DetailInfoPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (onPointer == false)
            DetailInfoPanel.SetActive(false);
    }
}
