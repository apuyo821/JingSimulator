//using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class ScheduleManager : MonoBehaviour
{
    public float actFlowTIme;
    public float actChgTime;
    public static int daycount = 0;
    public static int[] schedules;
    public GameObject[] SchedulePlace;

    [SerializeField] GameManager gameManager;
    buttonManager buttonManager;
    public GameObject[] UIObjects;  //0 : panel_select_task, 1 : panel_select
    public Text dDayTxT;
    public Text MonthWeekText;
    string WeekName;

    int dialoguLength;
    public bool isEvent = false;
    public bool isFirst = true;
    public bool isGO = false;
    public static bool isActing;
    public static bool isHome;
    [Header("대사 오브젝트")]
    [SerializeField] GameObject itrObj;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;
    [SerializeField] GameObject blurImage;
    [Header("징버거 SD")]
    public GameObject[] jingObjs;
    public ProcessBar processBar;

    [Header("아침 낮 저녁 변경")]
    [SerializeField] GameObject aCBackGround;
    [SerializeField] GameObject[] actChangeObjs;

    [Header("휴식 이벤트")]
    [SerializeField] GameObject restBackGround;
    [SerializeField] GameObject[] restEventObjs;

    int hpChangeValue,hpPreviusValue;

    [SerializeField] GameObject blackBG;

    //초기값 초기화
    void Awake()
    {
        schedules = new int[3];
    }


    //버튼매니저 오브제와 스크립트 불러오기
    private void Start()
    {
        GameObject btmObject = GameObject.Find("ButtonManager");
        buttonManager = btmObject.gameObject.GetComponent<buttonManager>();
        itrCs = itrObj.GetComponent<InteractionEvent>();
        dmCs = FindObjectOfType<DialogueManager>();

        UIObjects[2].SetActive(false);
        //게임 처음시작이거나 오디션이 끝났을 때 체크
        eventCheck(DataBase.DB.playerData.dDay);

        if (DataBase.DB.playerData.HP < 1)
        {
            buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
        }

        for (int i = 0; i < SchedulePlace.Length; i++)
        {
            SchedulePlace[i].SetActive(false);
        }
        SchedulePlace[0].SetActive(true);
        isHome = true;
        jingAnimControl.jingAnim.animPosSet(0);
        daycount = 0;
    }

    void OnEnable()
    {
        Setting();
    }

    //왼쪽 위 날짜 텍스트 설정 & HP가 0일 때 휴식 이벤트 실행되게 해주는 코드
    void Setting()
    {
        dDaySet(DataBase.DB.playerData.dDay);
        MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
    }

    //D-Day 텍스트 설정
    public void dDaySet(int _dDay)
    {
        
        if (_dDay == 0)
        {
            dDayTxT.text = "D-Day";
        }
        else
        {
            dDayTxT.text = "D" + "-" + _dDay.ToString();
        }
    }

    public void MonthWeekSet(int _week, int _month, int _day)
    {
        //week에 따라서 요일이 바뀜
        switch (_week)
        {
            case 0:
                WeekName = "일요일";
                break;
            case 1:
                WeekName = "월요일";
                break;
            case 2:
                WeekName = "화요일";
                break;
            case 3:
                WeekName = "수요일";
                break;
            case 4:
                WeekName = "목요일";
                break;
            case 5:
                WeekName = "금요일";
                break;
            case 6:
                WeekName = "토요일";
                break;
            default:
                break;
        }
        //바꾼 값 들로 텍스트 변환
        MonthWeekText.text = _month + "월 " + _day + "일 " + WeekName;
    }

    void WeekCalculate()
    {
        //n요일 계산
        //week가 7(없는 숫자)이면 0으로 만들어서 순환시키기
        DataBase.DB.playerData.week++;
        if (DataBase.DB.playerData.week == 7)
        {
            DataBase.DB.playerData.week = 0;
        }
    }

    void DayAndMonthCalculate()
    {
        //Day가 31(7월 31일)이라면 8월 1일로 만들어주는 코드
        if (DataBase.DB.playerData.Day == 31)
        {
            DataBase.DB.playerData.Day = 1;
            DataBase.DB.playerData.Month++;
        }
        else
        {
            DataBase.DB.playerData.Day++;
        }
    }

    //스케쥴 진행
    public void Processing()
    {
        //하나라도 선택되지 않았다면 일과가 시작 되지 않음
        if (schedules[0] != 0 &&
           schedules[1] != 0 &&
           schedules[2] != 0)
        {
            //일과 선택 패널 숨김
            UIObjects[0].SetActive(false);

            //행동 진행 중 다른 버튼 비활성화
            buttonManager.falseBtnItr();

            //행동 시작
            StartCoroutine(Process(schedules[daycount]));
        }
    }
    //n초의 시간 동안 행동 진행
    IEnumerator Process(int _actNum)
    {
        isHome = false;
        UIObjects[1].SetActive(false);
        SchedulePlace[0].SetActive(false);
        SchedulePlace[_actNum].SetActive(true);
        isActing = true;
        jingAnimControl.jingAnim.animPosSet(_actNum);
        processBar.processTimerStart();
        hpPreviusValue = DataBase.DB.playerData.HP;
        switch (_actNum)
        {
            //vocalTraning
            case 1:
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 1;
                DataBase.DB.playerData.vocal += 2;
                DataBase.DB.playerData.vocalCount++;
                DataBase.DB.playerData.rizz += 2;
                DataBase.DB.playerData.misukham += 1;
                break;

            //danceTraning
            case 2:
                DataBase.DB.playerData.HP  -= 8;
                DataBase.DB.playerData.MP -= 1;
                DataBase.DB.playerData.dance += 2;
                DataBase.DB.playerData.danceCount++;
                DataBase.DB.playerData.rizz += 2;
                DataBase.DB.playerData.misukham += 1;
                break;

            //broadcast
            case 3:
                DataBase.DB.playerData.HP -= 5;
                DataBase.DB.playerData.MP -= 4;
                DataBase.DB.playerData.rizz += 7;
                DataBase.DB.playerData.broadcastCount++;
                DataBase.DB.playerData.misukham += 1;
                break;

            //Game
            case 4:
                DataBase.DB.playerData.HP += 4;
                DataBase.DB.playerData.MP += 3;
                DataBase.DB.playerData.misukham++;
                DataBase.DB.playerData.gameCOunt++;
                break;

            //GYM
            case 5:
                DataBase.DB.playerData.HP += 3;
                DataBase.DB.playerData.MP += 1;
                DataBase.DB.playerData.strength += 2;
                DataBase.DB.playerData.GYMCount++;
                break;

            //Drawing
            case 6:
                DataBase.DB.playerData.HP += 2;
                DataBase.DB.playerData.MP += 2;
                DataBase.DB.playerData.deft += 4;
                DataBase.DB.playerData.misukham++;
                DataBase.DB.playerData.drawingCount++;
                break;

            //Hamburger
            case 7:
                DataBase.DB.playerData.HP -= 7;
                DataBase.DB.playerData.MP -= 5;
                DataBase.DB.playerData.deft += 2;
                DataBase.DB.playerData.money += 100;
                DataBase.DB.playerData.misukham++;
                break;

            default:
                break;
        }
        hpChangeValue = Mathf.Abs(DataBase.DB.playerData.HP - hpPreviusValue);

        //스탯 효과 점검
        yield return new WaitForSeconds(actFlowTIme / (float)2);
        statusCheck(DataBase.DB, hpChangeValue, _actNum);

        yield return new WaitUntil(() => isActing == false);
        daycount++;
        SchedulePlace[_actNum].SetActive(false);
        isActing = false;
        SchedulePlace[0].SetActive(true);
        isHome = true;
        jingAnimControl.jingAnim.animPosSet(0);
        for (int i = 0; i < HamPlace.consumerList.Count; i++)
        {
            Destroy(HamPlace.consumerList[i]);
        }

        //////////////////////////////////////////////////////////행동 배경 전환 과정
        aCBackGround.SetActive(true);
        if (daycount == 1)
        {
            actChangeObjs[1].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actChangeObjs[0].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actChangeObjs[2].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
        else if(daycount == 2)
        {
            actChangeObjs[3].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actChangeObjs[0].SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actChangeObjs[4].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        foreach (GameObject actChangeObj in actChangeObjs)
        {
            actChangeObj.SetActive(false);
        }
        aCBackGround.SetActive(false);
        ////////////////////////////////////////////////////////

        if (daycount < 3)
        {
            //daycount가 2 보다 작으면, 남은 행동 진행
            StartCoroutine(Process(schedules[daycount]));
        }
        else
        {
            //datcount가 2 보다 크면 하루 스케쥴 종료 및 스탯, 날짜 정산
            //jingAnimControl.jingAnim.animPosSet(0);
            daycount = 0;
            DataBase.DB.playerData.dDay--;

            //날짜 계산 및 가시화
            WeekCalculate();
            DayAndMonthCalculate();
            dDaySet(DataBase.DB.playerData.dDay);
            MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);

            //오디션 이벤트 발생
            if (DataBase.DB.playerData.dDay == 29 || DataBase.DB.playerData.dDay == 16 || DataBase.DB.playerData.dDay == 0)
            {
                switch (DataBase.DB.playerData.dDay)
                {
                    case 29:
                        DataBase.DB.playerData.auditionIndex = 0;
                        break;

                    case 16:
                        DataBase.DB.playerData.auditionIndex = 1;
                        break;

                    case 0:
                        DataBase.DB.playerData.auditionIndex = 2;
                        break;

                    default:
                        break;
                }
                //fist, second, third Audition
                buttonManager.btn[0].interactable = false;
                buttonManager.btn[4].gameObject.SetActive(true);
            }

            //대사 이벤트 확인 및 진행
            eventCheck(DataBase.DB.playerData.dDay);
            yield return new WaitUntil(() => isGO == true);

            UIObjects[1].SetActive(true);

            //선택 된 행동들 리셋 및 비활성화된 버튼들 다시 활성화
            for (int index = 0; index < schedules.Length; index++)
            {
                schedules[index] = 0;
            }
            buttonManager.trueBtnItr();

            //체력 또는 정신력이 0이 아니라면 체력과 정신력 회복
            if(DataBase.DB.playerData.HP > 1 && DataBase.DB.playerData.MP > 1)
            {
                DataBase.DB.playerData.HP += 4;
                DataBase.DB.playerData.MP += 2;
            }

            subActEventCheck();
        }
    }

    public void subActEventCheck()
    {
        if (DataBase.DB.playerData.GYMCount >= 15 && DataBase.DB.playerData.isGYMEvent == false)
        {
            DataBase.DB.eventType = 0;
            SceneManager.LoadScene("Event");
        }
        else if (DataBase.DB.playerData.gameCOunt >= 15 && DataBase.DB.playerData.isGameEvent == false)
        {
            DataBase.DB.eventType = 1;
            SceneManager.LoadScene("Event");
        }
        else if (DataBase.DB.playerData.drawingCount >= 15 && DataBase.DB.playerData.isDrawingEvent == false)
        {
            DataBase.DB.eventType = 2;
            SceneManager.LoadScene("Event");
        }
    }

    public bool eventCheck(int _dday)
    {
        switch (_dday)
        {
            case 40:
                if (isFirst)
                {
                    isGO = false;
                    isEvent = true;
                    itrCs.dialogueEvent.line.x = 1;
                    itrCs.dialogueEvent.line.y = 3;
                    dialoguLength = 3;
                    itrCs.dialogueEvent.name = "스토리 설명 1";
                    blackBG.SetActive(true);
                    StartCoroutine(afterFirstStory());
                }
                break;

            case 38:
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 13;
                itrCs.dialogueEvent.line.y = 17;
                dialoguLength = 5;
                itrCs.dialogueEvent.name = "알바 설명";
                break;

            case 28:
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 18;
                itrCs.dialogueEvent.line.y = 22;
                dialoguLength = 5;
                itrCs.dialogueEvent.name = "1차 오디션 끝난 후의 대화";
                break;

            case 15:
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 23;
                itrCs.dialogueEvent.line.y = 29;
                dialoguLength = 7;
                itrCs.dialogueEvent.name = "2차 오디션 끝난 후의 대화";
                break;

            case 1:
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 30;
                itrCs.dialogueEvent.line.y = 36;
                dialoguLength = 7;
                itrCs.dialogueEvent.name = "3차 오디션 끝난 후의 대화";
                break;

            default:
                isGO = true;
                break;
        }
        if (isEvent)
        {
            UIObjects[2].SetActive(true);
            buttonManager.falseBtnItr();
            itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
            dmCs.ShowDialogue(itrCs.GetDialogue());
        }

        return isGO;
    }

    IEnumerator afterFirstStory()
    {
        yield return new WaitUntil(() => isEvent == false);
        UIObjects[2].SetActive(true);
        blackBG.SetActive(false);
        isFirst = false;
        isGO = false;
        isEvent = true;
        itrCs.dialogueEvent.line.x = 4;
        itrCs.dialogueEvent.line.y = 11;
        dialoguLength = 8;
        itrCs.dialogueEvent.name = "스토리 설명 2";

        if (isEvent)
        {
            buttonManager.falseBtnItr();
            itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
            dmCs.ShowDialogue(itrCs.GetDialogue());
        }

        yield return new WaitUntil(() => isEvent == false);
        StartCoroutine(tutorial());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator tutorial()
    {
        //혹시 첫 번째 오디션은 뭘 준비해야 하나요?
        //위의 대사 다음 스케쥴 패널을 띄우고 설명하는 장면
        blackBG.SetActive(false);
        UIObjects[2].SetActive(true);
        isFirst = false;
        isGO = false;
        isEvent = true;
        itrCs.dialogueEvent.line.x = 12;
        itrCs.dialogueEvent.line.y = 12;
        dialoguLength = 1;
        itrCs.dialogueEvent.name = "튜토리얼";

        if (isEvent)
        {
            buttonManager.falseBtnItr();
            itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
            dmCs.ShowDialogue(itrCs.GetDialogue());
        }
        bool first = true, second = false;
        Color changeColor = new Color(255, 255, 255, 0.75f);
        GameObject dialoguePanel = GameObject.Find("dialoguePanel");
        Image dialoguePanelImage = dialoguePanel.GetComponent<Image>();
        while (isEvent)
        {
            if (Input.anyKeyDown)
            {
                if (first)
                {
                    first = false;
                    blurImage.SetActive(false);
                    second = true;
                }
                else if (second)
                {
                    UIObjects[0].SetActive(true);
                    dialoguePanelImage.color = changeColor;
                    second = false;
                }
            }
            yield return null;
        }
        changeColor = new Color(255, 255, 255, 1f);
        dialoguePanelImage.color = changeColor;
        blurImage.SetActive(true);
        UIObjects[0].SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    void statusCheck(DataBase data, int _changeValue, int _actNum)
    {
        int statusType = Random.Range(0,3), gacha;
        bool goal = false;
        switch (statusType)
        {
            //str
            case 0:
                if((_actNum >= 1 && _actNum <=3) || _actNum == 8 || _actNum == 9)
                {
                    gacha = Random.Range(1, 101);
                    if (goal = (gacha <= data.playerData.strength))
                        data.playerData.HP += _changeValue;
                }
                break;

            //deft
            case 1:
                if(_actNum == 7)
                {
                    gacha = Random.Range(1, 101);
                    if (goal = (gacha <= data.playerData.deft))
                        data.playerData.money += 50;
                }
                break;

            //misukham
            case 2:
                gacha = Random.Range(1, 101);
                if (goal = (gacha * 2 <= Mathf.Abs(data.playerData.misukham)))
                    data.playerData.MP -= 4;
                break;

            default:
                break;
        }
        if (goal)
        {
            GameObject statusIcon = Instantiate(jingObjs[1], jingObjs[0].transform);
            statusIcon.transform.parent = jingObjs[0].transform;
            statusEffect statusIconCs = statusIcon.GetComponent<statusEffect>();
            statusIconCs.statusType = statusType;
            statusIconCs.iconMoving();
        }
    }

    //오디션 진행
    public void AuditionPricessing()
    {
        SceneManager.LoadScene("Audition");
    }

    //스케쥴 버튼 눌렀을 때, HP나 MP를 검사하여 '휴식' 이벤트 판단하는 코드
    //True일 경우 휴식 이벤트 실행, False일 경우 정상 진행
    public void CheckHpMP()
    {
        if(DataBase.DB.playerData.HP == 0 || DataBase.DB.playerData.MP == 0)
        {
            StartCoroutine(IsZero());
        }
        else
        {
            UIObjects[0].SetActive(true);
        }
    }

    //체력 or MP가 0일 때의 이벤트
    IEnumerator IsZero()
    {
        DataBase.DB.playerData.dDay--;
        DataBase.DB.playerData.HP += 13;
        DataBase.DB.playerData.MP += 6;

        restBackGround.SetActive(true);
        restEventObjs[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        restEventObjs[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        restEventObjs[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject restEvent in restEventObjs)
        {
            restEvent.SetActive(false);
        }
        restBackGround.SetActive(false);

        WeekCalculate();
        DayAndMonthCalculate();
        dDaySet(DataBase.DB.playerData.dDay);
        MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);

    }
}