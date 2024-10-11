using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acceptButton : MonoBehaviour
{
    [SerializeField] ScheduleManager scheduleManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            scheduleManager.Processing();
    }
}
