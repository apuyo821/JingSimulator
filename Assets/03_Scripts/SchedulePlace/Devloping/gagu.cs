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
        Devloping devloping = other.gameObject.GetComponent<Devloping>();
        isEventing = true;
        Rigidbody rigid = other.attachedRigidbody;
        if(rigid.velocity.y == 0)
        {
            StartCoroutine(checkTime());
        }
        else if(rigid.velocity.y < 0)
        {
            if(!devloping.isHanging)
                Debug.Log("À§¿¡¼­ ¶³¾îÁü");
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
            Debug.Log("2ÃÊ Áö³²");
    }
}
