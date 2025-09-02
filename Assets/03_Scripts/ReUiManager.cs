using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReUiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] UIObjs;          // 반응형 Ui를 담은 배열

    private void Start()
    {
        UISet(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            UISet(false);
    }

    void UISet(bool p_flag)
    {
        for (int i = 0; i < UIObjs.Length; i++)
        {
            UIObjs[i].SetActive(p_flag);
        }
    }
}
