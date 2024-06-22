using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class dragObj : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Transform parent_after_drag;
    public int actNum;
    public GameObject emptyObj;
    public Transform spawnPosition;

    GameObject instance;


    public void OnBeginDrag(PointerEventData eventData)
    {
        instance = Instantiate(emptyObj, spawnPosition);
        instance.transform.SetParent(parent_after_drag);
        parent_after_drag = transform.parent;   //Drag가 끝났을 때 원래 자리로 돌아가기 위해서 위치 저장
        //Debug.Log(parent_after_drag);
        transform.SetParent(transform.root);    //하이라키에서 오브젝트가 부모가 되게 만들어줌
        transform.SetAsLastSibling();           //해당 오브젝트의 순위를 마지막으로 변경(가장 나중에 출력되므로 겹쳐졌을 경우 앞으로 나옵니다.)

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(instance);
        transform.SetParent(parent_after_drag); //Drag가 끝나고 Drag 시작 전의 위치로 되돌아가기
        image.raycastTarget = true;
    }
}
