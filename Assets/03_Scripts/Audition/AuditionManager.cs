using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionManager : MonoBehaviour
{
    public GameObject[] rythmGameSystem;

    private void Start()
    {
        for (int i = 0; i < rythmGameSystem.Length; i++)
        {
            rythmGameSystem[i].SetActive(false);
        }

        Debug.Log(DataBase.DB.playerData.auditionIndex);

        if (DataBase.DB.playerData.auditionIndex == 0)
            rythmGameSystem[0].SetActive(true);
        else if (DataBase.DB.playerData.auditionIndex == 1)
            rythmGameSystem[1].SetActive(true);
    }
}
