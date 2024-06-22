using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerEvent : MonoBehaviour
{
    [SerializeField] int regenTime;
    [SerializeField] GameObject consumerObj;
    [SerializeField] Transform HamTransform;
    [SerializeField] Transform SpawnPosition;

    private void OnEnable()
    {
        StartCoroutine(spawnObj());
    }

    IEnumerator spawnObj()
    {
        while (gameObject.activeSelf)
        {
            GameObject consumerClone = Instantiate(consumerObj, SpawnPosition.position, transform.rotation);
            consumerClone.transform.parent = HamTransform;
            consumerClone.GetComponent<Consumer>().begin();
            regenTime = Random.Range(6, 8);
            yield return new WaitForSeconds(regenTime);
        }
    }
}
