using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditUIControl : MonoBehaviour
{
    [SerializeField] GameObject[] panels;

    private void OnEnable()
    {
        foreach (GameObject item in panels)
        {
            item.SetActive(false);
        }
    }
}
