using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devloping : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] int veloX;
    [SerializeField] int coolTime;

    public bool isHanging;
    Vector3 mousePosition;

    private void Start()
    {
        StartCoroutine(Moving());
        StartCoroutine(veloControl());
    }

    IEnumerator Moving()
    {
        while (false)
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
            veloX = Random.Range(-1, 2)*2;
        }
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        isHanging = true;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).y, -10f);
        if(transform.position.y < 1.5f)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).x, 1.5f, -10f);
        }
    }

    private void OnMouseUp()
    {
        isHanging = false;
    }
}
 