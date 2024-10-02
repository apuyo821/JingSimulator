using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class omuriceFlying : MonoBehaviour
{

    int x = 0, y = 0;

    bool isFlying = false;

    private void OnEnable()
    {
        isFlying = true;
        StartCoroutine(transTaget());
    }

    private void OnDisable()
    {
        isFlying = false;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x,y,0), 4f);

        if(Mathf.Abs(transform.localPosition.x) >= 800 || Mathf.Abs(transform.localPosition.y) >= 500)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    IEnumerator transTaget()
    {
        while (isFlying)
        {
            x = Random.Range(-6, 7) * 100;
            y = Random.Range(-5, 6) * 100;

            yield return new WaitForSeconds(2f);
        }


        yield return null;
    }
}
