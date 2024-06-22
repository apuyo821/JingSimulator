using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleSetting : MonoBehaviour
{
    [SerializeField] GameObject[] scripts;
    [Tooltip("0~15")] [SerializeField] int number;

    void Start()
    {
        scripts[number].SetActive(true);
    }
}
