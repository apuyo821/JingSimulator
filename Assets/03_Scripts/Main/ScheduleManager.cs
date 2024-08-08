using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScheduleManager : MonoBehaviour
{
    [SerializeField] float actFlowTIme;
    [SerializeField] float actChgTime;
    public int daycount = 0;
    public static int[] schedules;
    public CinemachineVirtualCamera[] vCams;
    //public static bool isActing;

    buttonManager buttonManager;
    public GameObject panel_select_task;
    public Text dDayTxT;
    public Text MonthWeekText;
    string WeekName;

    int dialoguLength;
    public bool isEvent = false;
    public bool isFirst = true;
    public bool isGO = false;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;

    //초기값 초기화
    void Awake()
    {
        schedules = new int[3];
        //isActing = false;
    }

    //버튼매니저 오브제와 스크립트 불러오기
    private void Start()
    {
        GameObject btmObject = GameObject.Find("ButtonManager");
        buttonManager = btmObject.gameObject.GetComponent<buttonManager>();
        itrCs = GameObject.FindGameObjectWithTag("DialogueObj").GetComponent<InteractionEvent>();
        dmCs = FindObjectOfType<DialogueManager>();

        if (isFirst)
        {
            eventCheck(DataBase.DB.playerData.dDay);
        }

        if (DataBase.DB.playerData.HP < 1)
        {
            buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
        }
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
        if (DataBase.DB.playerData.HP < 1)
        {
            buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
        }
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
            GameObject.Find("panel_select_task").SetActive(false);

            //행동 진행 중 다른 버튼 비활성화
            buttonManager.falseBtnItr();

            //행동 시작
            StartCoroutine(Process(schedules[daycount]));
        }
    }
    //n초의 시간 동안 행동 진행
    IEnumerator Process(int _actNum)
    {
        switch (_actNum)
        {
            //workout
            case 0:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.strength += 2;
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 1;
                break;

            //drawing
            case 1:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.deft += 2;
                DataBase.DB.playerData.HP -= 1;
                break;

            //vocalTraning
            case 2:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.rizz++;
                DataBase.DB.playerData.vocal++;
                DataBase.DB.playerData.HP -= 2;
                DataBase.DB.playerData.MP -= 1;
                break;

            //danceTraning
            case 3:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.rizz++;
                DataBase.DB.playerData.dance++;
                DataBase.DB.playerData.HP -= 3;
                DataBase.DB.playerData.MP -= 1;
                break;

            //actTraining
            case 4:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.rizz += 2;
                DataBase.DB.playerData.HP -= 2;
                DataBase.DB.playerData.MP -= 2;
                break;

            //skinCare
            case 5:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.rizz += 4;
                DataBase.DB.playerData.MP += 1;
                break;

            //미정
            case 6:
                vCams[_actNum].Priority = 11;
                break;

            //Game
            case 7:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.HP += 4;
                DataBase.DB.playerData.MP += 3;
                DataBase.DB.playerData.game += 2;
                break;

            //WalkPark
            case 8:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.HP += 1;
                DataBase.DB.playerData.MP += 5;
                break;

            case 9:
                vCams[_actNum].Priority = 11;
                DataBase.DB.playerData.HP += 1;
                DataBase.DB.playerData.MP += 5;
                break;

            default:
                break;
        }
        daycount++;

        yield return new WaitForSeconds(actFlowTIme); //행동 진행 시간
        vCams[_actNum].Priority = 5;
        yield return new WaitForSeconds(actChgTime); //배경 전환 시간, 집으로 카메라 바뀌었다가 행동 배경으로 전환
        
        if(daycount < 3)
        {
            //daycount가 2 보다 작으면, 남은 행동 진행
            StartCoroutine(Process(schedules[daycount]));
        }
        else
        {
            //datcount가 2 보다 크면 하루 스케쥴 종료 및 스탯, 날짜 정산
            daycount = 0;
            DataBase.DB.playerData.dDay--;

            //날짜 계산 및 가시화
            WeekCalculate();
            DayAndMonthCalculate();
            dDaySet(DataBase.DB.playerData.dDay);
            MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);

            //대사 이벤트 확인 및 진행
            eventCheck(DataBase.DB.playerData.dDay);
            yield return new WaitUntil(() => isGO == true);

            //선택 된 행동들 리셋 및 비활성화된 버튼들 다시 활성화
            for (int index = 0; index < schedules.Length; index++)
            {
                schedules[index] = 0;
            }
            buttonManager.trueBtnItr();

            //오디션 이벤트 발생
            if (DataBase.DB.playerData.dDay == 39 || DataBase.DB.playerData.dDay == 26 || DataBase.DB.playerData.dDay == 19)
            {
                //fist, second, third Audition
                buttonManager.btn[0].interactable = false;
                buttonManager.btn[4].gameObject.SetActive(true);
            }
            else if(DataBase.DB.playerData.dDay == 0)
            {
                //Final Audition
                buttonManager.btn[0].interactable = false;
                buttonManager.btn[4].gameObject.SetActive(true);
                buttonManager.btn[5].gameObject.SetActive(true);
            }
            //HP가 0일 때 휴식 이벤트
            if (DataBase.DB.playerData.HP < 1)
            {
                buttonManager.btn[0].GetComponentInChildren<Text>().text = "휴식";
            }
        }
    }

    public bool eventCheck(int _dday)
    {
        Debug.Log(_dday);
        switch (_dday)
        {
            case 50:
                isFirst = false;
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 1;
                itrCs.dialogueEvent.line.y = 3;
                dialoguLength = 3;
                itrCs.dialogueEvent.name = "튜토리얼";
                break;

            case 48:
                isGO = false;
                isEvent = true;
                itrCs.dialogueEvent.line.x = 4;
                itrCs.dialogueEvent.line.y = 6;
                dialoguLength = 3;
                itrCs.dialogueEvent.name = "뢴트와 왁의 대화";
                break;

            default:
                isGO = true;
                break;
        }
        if (isEvent)
        {
            buttonManager.falseBtnItr();
            itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
            dmCs.ShowDialogue(itrCs.GetDialogue());
        }

        return isGO;
    }

    //오디션 진행
    public void AuditionPricessing()
    {
        SceneManager.LoadScene("auditionScene");
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
            panel_select_task.SetActive(true);
        }
    }

    //체력 or MP가 0일 때의 이벤트
    IEnumerator IsZero()
    {
        vCams[10].Priority = 11;
        DataBase.DB.playerData.dDay--;
        DataBase.DB.playerData.HP += 25;
        DataBase.DB.playerData.MP += 13;
        yield return new WaitForSeconds(4.0f);
        vCams[10].Priority = 5;
        dDaySet(DataBase.DB.playerData.dDay);
        MonthWeekSet(DataBase.DB.playerData.week, DataBase.DB.playerData.Month, DataBase.DB.playerData.Day);
        buttonManager.btn[0].GetComponentInChildren<Text>().text = "스케쥴";
    }
}