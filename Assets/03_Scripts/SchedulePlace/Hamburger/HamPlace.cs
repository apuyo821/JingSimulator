using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamPlace : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int processIndex = 0;

    Vector3 spawnPoint = new Vector3(16, -2.6f, 0);

    public static List<GameObject> consumerList = new List<GameObject>();

    private void OnEnable()
    {
        consumerList.Clear();
        GameObject consumer = Instantiate(prefab, spawnPoint, transform.rotation);
        consumerList.Add(consumer);
        consumer.transform.parent = transform;
        processIndex++;
    }
}
