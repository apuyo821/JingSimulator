using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devloping : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] int veloX;
    [SerializeField] int coolTime;

    private void Start()
    {
        StartCoroutine(Moving());
        StartCoroutine(veloControl());
    }

    IEnumerator Moving()
    {
        while (true)
        {
            rigid.velocity = new Vector3(veloX*2, rigid.velocity.y, rigid.velocity.z);
            yield return null;
        }
    }

    IEnumerator veloControl()
    {
        while (true)
        {
            coolTime = Random.Range(1, 4);
            yield return new WaitForSeconds(coolTime);
            veloX = Random.Range(-1, 2);
        }
    }
}
