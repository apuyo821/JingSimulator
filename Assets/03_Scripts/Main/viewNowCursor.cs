using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewNowCursor : MonoBehaviour
{
    [SerializeField] RectTransform cursorRect;
    [SerializeField] GameObject scheduleManagerObj;
    [SerializeField] ScheduleManager scheduleManagerCs;
    [SerializeField] Image image;

    int schedules = 0;

    //UI의 트랜스폼은 RectTransform을 써야하기에 이것만 빼면은 일반 오브젝트의 Transform을 가져오는 것과 같음
    void Start()
    {
        cursorRect = GetComponent<RectTransform>();
        scheduleManagerCs = scheduleManagerObj.GetComponent<ScheduleManager>();
        image = GetComponent<Image>();
        image.enabled = false;
        StartCoroutine(imgPositioning());
        
    }

    //ScheduleManager의 daycount에 따라서 지금 하는 행동을 가리키는 커서의 위치 변경
    void viweImgPosition()
    {
        schedules = ScheduleManager.daycount;
        switch (schedules+1)
        {
            case 0:
                image.enabled = false;
                break;

            case 1:
                image.enabled = true;
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

    IEnumerator imgPositioning()
    {
        while (true)
        {
            schedules = ScheduleManager.daycount;
            yield return new WaitUntil(() => ScheduleManager.isActing == true);
            switch (schedules + 1)
            {

                case 1:
                    image.enabled = true;
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
            yield return new WaitUntil(() => ScheduleManager.isActing != true);
            if (schedules == 2)
                image.enabled = false;
            yield return null;
        }
    }
}
