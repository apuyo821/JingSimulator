using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class RGManager : MonoBehaviour
{
    public static RGManager RGinstance;
    List<Notes> noteCsList;
    public List<Image> noteImageList;
    public Camera mainCamera;

    public GameObject[] Objs;
    public Timer timerCS;

    public Text[] texts;
    public TMP_Text bonusStatText;

    public int miss = 0, processIndex = 0;
    public bool clear = false, isStart = false;
    int noteAmount;
    int timeAmount = 3;

    public GameObject explainPanel;

    [Header("audiitonGradeExplainPanel")]
    [SerializeField] GameObject audiitonGradeExplainPanel;
    [SerializeField] TMP_Text auditionGradeExplainText;

    [SerializeField] GameObject resultPanel;
    [Header("stage Object")]
    [SerializeField] GameObject showStagePanel;
    [SerializeField] TMP_Text showStageText;

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographic = true;
        RGinstance = this;
        timerCS = Objs[3].GetComponent<Timer>();
        isStart = false;
        showGradeExplainPanel();
        resultPanel.SetActive(false);
        showStagePanel.SetActive(false);
    }

    void showGradeExplainPanel()
    {
        int _danceStat = DataBase.DB.playerData.dance + Mathf.RoundToInt((float)DataBase.DB.playerData.rizz * 0.2f);
        Debug.Log(Mathf.RoundToInt((float)DataBase.DB.playerData.rizz * 0.2f));
        if (_danceStat > 40)
            noteAmount = 8;
        else if (_danceStat <= 40 && _danceStat >= 30)
            noteAmount = 10;
        else if (_danceStat < 30)
            noteAmount = 16;

        switch (noteAmount)
        {
            case 8:
                auditionGradeExplainText.text = "오늘은 오디션에\n무조건 통과하겠는걸";
                break;

            case 10:
                auditionGradeExplainText.text = "준비는 잘 해온거 같아\n이제 실전만 남았어";
                break;

            case 14:
                auditionGradeExplainText.text = "큰일이야, 시간이\n부족했던 거 같은데...";
                timerCS.time = 10;
                timerCS.slider.maxValue = 10;
                break;

            default:
                break;
        }
        audiitonGradeExplainPanel.SetActive(true);
    }

    public void showExplainPanel()
    {
        audiitonGradeExplainPanel.SetActive(false);
        explainPanel.SetActive(true);
    }

    public void BeforeGameStart()
    {
        explainPanel.SetActive(false);
        StartCoroutine(SecondAuditionProcess());
    }

    public void winOrLose(int _or)
    {
        StartCoroutine(brawl(_or));
        Objs[4].SetActive(true);
    }

    public void GoMain()
    {
        DataBase.DB.isAuditionEnd = true;
        SceneManager.LoadScene("Main");
    }

    IEnumerator settingAndStart()
    {
        noteImageList = new List<Image>();
        noteCsList = new List<Notes>();

        for (int i = 0; i < noteAmount; i++)
        {
            GameObject notes = Instantiate(Objs[0]);
            Image noteImage = notes.GetComponent<Image>();
            Notes notesCS = notes.GetComponent<Notes>();
            notes.transform.SetParent(Objs[1].transform);
            notes.transform.localScale = new Vector3(1f, 1f, 1f);
            noteCsList.Add(notesCS);
            notesCS.lastNoteIndex = noteAmount;
            notesCS.index = i;
            noteImage.enabled = false;
            switch (noteAmount)
            {
                case 8:
                case 10:
                case 12:
                    notesCS.typeNum = Random.Range(0, 4);
                    break;

                case 9:
                case 11:
                case 13:
                    notesCS.typeNum = Random.Range(0, 6);
                    break;

                case 7:
                    notesCS.typeNum = Random.Range(0, 10);
                    break;

                default:
                    break;
            }
            notesCS.TypeSetting();
            noteImageList.Add(noteImage);
            //noteImage.enabled = false;
        }
        yield return new WaitUntil(() => isStart == true);
        for (int i = 0; i < noteCsList.Count; i++)
        {
            noteCsList[i].startProcess();
            noteImageList[i].enabled = true;
        }
    }

    IEnumerator TTO()
    {
        Debug.Log("d");
        Objs[2].SetActive(true);
        isStart = false;
        timeAmount = 3;
        while (timeAmount != 0)
        {
            texts[2].text = timeAmount.ToString();
            yield return new WaitForSeconds(1f);
            timeAmount--;
        }
        Objs[2].SetActive(false);
        yield return null;
        isStart = true;
        timerCS.timerStart();
        timerCS.slider.value = 0;
    }

    IEnumerator brawl(int _or)
    {
        texts[_or].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        texts[_or].gameObject.SetActive(false);
    }

    IEnumerator SecondAuditionProcess()
    {
        yield return null;
        processIndex = 1;
        showStagePanel.SetActive(true);
        showStageText.text = processIndex.ToString() + " 스테이지";
        yield return new WaitForSeconds(1f);
        showStagePanel.SetActive(false);

        //first Stage, index = 0
        StartCoroutine(TTO());
        StartCoroutine(settingAndStart());

        yield return new WaitUntil(() => clear == true);
        processIndex++;
        showStagePanel.SetActive(true);
        showStageText.text = processIndex.ToString() + " 스테이지";
        timerCS.timeStop();
        noteAmount += 2;
        clear = false;
            
        yield return new WaitForSeconds(1f);
        showStagePanel.SetActive(false);

        //Second Stage, index = 1
        StartCoroutine(TTO());
        StartCoroutine(settingAndStart());

        yield return new WaitUntil(() => clear == true);
        processIndex++;
        showStagePanel.SetActive(true);
        timerCS.timeStop();
        showStageText.text = processIndex.ToString() + " 스테이지";
        noteAmount += 2;
        clear = false;

        yield return new WaitForSeconds(1f);
        showStagePanel.SetActive(false);

        //Last Stage, index = 2
        StartCoroutine(TTO());
        StartCoroutine(settingAndStart());

        yield return new WaitUntil(() => clear == true);
        timerCS.timeStop();
    }
}
