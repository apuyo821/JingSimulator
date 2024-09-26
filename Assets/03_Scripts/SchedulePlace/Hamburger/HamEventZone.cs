using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamEventZone : MonoBehaviour
{
    public static bool isHamEnd = false;
    int staffNum = 0;
    bool judgeBool;

    public bool judge(int _consumerNum)
    {
        staffNum = Random.Range(0, 3);
        if (_consumerNum == staffNum)
        {
            judgeBool = true;
        }
        else
            judgeBool = false;

        return judgeBool;
    }

    private void OnEnable()
    {
        isHamEnd = false;
    }

    private void OnDisable()
    {
        isHamEnd = false;
    }
}
