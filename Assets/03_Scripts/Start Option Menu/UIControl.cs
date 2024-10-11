using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject[] uiOBjs;

    private void Start()
    {
        for (int i = 0; i < uiOBjs.Length; i++)
        {
            uiOBjs[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.audioManager.sfx[6].Play();
            for (int i = 0; i < uiOBjs.Length; i++)
            {
                uiOBjs[i].SetActive(false);
            }
        }
    }
}
