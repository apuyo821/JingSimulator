using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreEvent : MonoBehaviour
{
    [SerializeField] int regenTime;
    [SerializeField] GameObject consumerObj;
    [SerializeField] Transform storeTransform;

    private void OnEnable()
    {
        StartCoroutine(spawnObj());
    }

    IEnumerator spawnObj()
    {
        while (gameObject.activeSelf)
        {
            GameObject consumerClone = Instantiate(consumerObj, new Vector3(8.5f, 1.5f, -2.0f), transform.rotation);
            consumerClone.transform.parent = storeTransform;
            consumerClone.GetComponent<Consumer>().begin();
            yield return new WaitForSeconds(regenTime);
            regenTime = Random.Range(2, 4);
        }
    }
}
