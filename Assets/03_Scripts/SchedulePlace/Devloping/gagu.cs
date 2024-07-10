using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class gagu : MonoBehaviour
{
    [SerializeField] bool isEventing;
    

    private void OnTriggerEnter(Collider other)
    {
        isEventing = true;
        Rigidbody rigid = other.attachedRigidbody;
        if(rigid.velocity.y == 0)
        {
            Debug.Log("수평 이벤트");
            StartCoroutine(checkTime());
        }
        else if(rigid.velocity.y < 0)
        {
            Debug.Log("위에서 떨어짐");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isEventing = false;
    }

    IEnumerator checkTime()
    {
        yield return new WaitForSecondsRealtime(2);
        if(isEventing)
            Debug.Log("2초 지남");
    }
}
