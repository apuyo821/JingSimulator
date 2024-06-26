using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewNowCursor : MonoBehaviour
{
    [SerializeField] RectTransform cursorRect;
    [SerializeField] GameObject scheduleManagerObj;
    [SerializeField] ScheduleManager scheduleManagerCs;

    int schedules;

    void Start()
    {
        cursorRect = GetComponent<RectTransform>();
        scheduleManagerCs = scheduleManagerObj.GetComponent<ScheduleManager>();
    }

    //ScheduleManager의 daycount에 따라서 지금 하는 행동을 가리키는 커서의 위치 변경
    void viweImgPosition()
    {
        schedules = scheduleManagerCs.daycount;
        switch (schedules)
        {
            case 1:
                cursorRect.anchoredPosition = new Vector2(115, -318);
                break;

            case 2:
                cursorRect.anchoredPosition = new Vector2(185, -318);
                break;

            case 3:
                cursorRect.anchoredPosition = new Vector2(255, -318);
                break;

            default:
                break;

        }
    }

    private void Update()
    {
        viweImgPosition();
    }
}
