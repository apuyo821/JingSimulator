using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABackGroundMove : MonoBehaviour
{
    [SerializeField] GameObject backGround;
    [SerializeField] GameObject resultButton;
    [SerializeField] float velo = 0;

    public void startBGMoving()
    {
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        while (resultButton.activeSelf == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * velo * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
}
