using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TaskSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DetailInfoPanel;
    IEnumerator ActionCoroutine;

    //포인터가 들어왔을 때, 진행
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActionCoroutine = Action();
        StartCoroutine(ActionCoroutine);
    }
    
    //slot위에 일정 시간 동안 올려놨을 때, 세부사항 띄우기
    IEnumerator Action()
    {
        yield return new WaitForSeconds(0.5f);
        DetailInfoPanel.SetActive(true);
    }

    //포인터가 벗어 날 때, 초기화
    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(ActionCoroutine);
        DetailInfoPanel.SetActive(false);
    }
}
