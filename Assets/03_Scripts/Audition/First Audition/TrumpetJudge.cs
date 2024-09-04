using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrumpetJudge : MonoBehaviour
{
    [SerializeField] TrumpetUI trumpetUI;
    [SerializeField] TrumpetNote noteCs;
    public Collider2D coll;
    public GameObject resultButton;
    public GameObject explainPanel;

    int Miss;

    public string rank;
    public int combo;

    [SerializeField] int noteIndex;
    [SerializeField] int score;
    [SerializeField] string resultRank;

    [SerializeField] GameObject TimeCountPanel;
    [SerializeField] Text timeCountTxt;
    public GameObject[] beforeNoteObjs;
    public TrumpetNote[] beforeNoteCss;

    public List<GameObject> notenote = new List<GameObject>();

    public Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.orthographic = true;
        beforeNoteObjs = GameObject.FindGameObjectsWithTag("Note");
        beforeNoteCss = new TrumpetNote[beforeNoteObjs.Length];
        for (int i = 0; i < beforeNoteCss.Length; i++)
        {
            beforeNoteCss[i] = beforeNoteObjs[i].gameObject.GetComponent<TrumpetNote>();
        }
        noteVeloSet(0);
        explainPanel.SetActive(true);
        Miss = 0;
        combo = 0;
        resultButton.SetActive(false);
    }

    public void beforeStart()
    {
        explainPanel.SetActive(false);
        StartCoroutine(TimeCount());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        noteCs = collision.gameObject.GetComponentInParent<TrumpetNote>();
        coll = collision.gameObject.GetComponent<Collider2D>();
        switch (collision.transform.tag)
        {
            case "noteHead":
                if (noteCs.headHit  != true)
                {
                    noteCs.headHit = false;
                    Destroy(coll);
                }
                break;

            case "noteFoot":
                if (noteCs.footHit != true)
                {
                    noteCs.footHit = false;
                    Destroy(coll);
                }
                noteCs.DestroyTimer();
                noteIndex++;
                scoreJudge();
                break;

            default:
                break;
        }
    }

    void scoreJudge()
    {
        if(noteCs.headHit == true && noteCs.noteTime > 2 && noteCs.footHit == true)
        {
            score += 500;
            combo++;
            rank = "Perfect";
        }
        else if(noteCs.headHit == false && noteCs.noteTime >= 2 && noteCs.footHit == true)
        {
            score += 300;
            combo++;
            rank = "Good";
        }
        else if (noteCs.headHit == false && noteCs.noteTime < 2 && noteCs.footHit == true)
        {
            score += 100;
            combo = 0;
            rank = "OK";
        }
        else if (noteCs.headHit == false && noteCs.footHit == false)
        {
            Miss++;
            combo = 0;
            rank = "Miss";
        }

        if(noteIndex == 5)
        {
            scoreCalculate();
            trumpetUI.showResultPanel(resultRank);
            resultButton.SetActive(true);
        }
    }

    void scoreCalculate()
    {
        if (score >= 2000)
        {
            resultRank = "1등";
            DataBase.DB.playerData.firstPlace++;
            DataBase.DB.playerData.rankScore += 1;
        }
        else if (score < 2000 && score >= 1500)
        {
            resultRank = "2등";
            DataBase.DB.playerData.rankScore += 2;
        }
        else if (score < 1500 && score >= 500)
        {
            resultRank = "3등";
            DataBase.DB.playerData.rankScore += 3;
        }
        else if (score < 500 && score >= 0)
        {
            resultRank = "4등";
            DataBase.DB.playerData.rankScore += 4;
        }
        else if (score <= 0)
            resultRank = "탈락";
    }

    void noteVeloSet(int _velo)
    {
        for (int i = 0; i < beforeNoteObjs.Length; i++)
        {
            beforeNoteCss[i].velo = _velo;
        }
    }

    void removeObjs()
    {
        System.Array.Clear(beforeNoteObjs, 0, beforeNoteObjs.Length);
        System.Array.Clear(beforeNoteCss, 0, beforeNoteCss.Length);
    }

    public void GoMain()
    {
        DataBase.DB.playerData.auditionIndex++;
        SceneManager.LoadScene("Main");
    }

    IEnumerator TimeCount()
    {
        int time = 3;
        TimeCountPanel.SetActive(true);
        while(time != 0)
        {
            timeCountTxt.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        TimeCountPanel.SetActive(false);
        yield return null;
        //noteVeloSet(10);
        if (DataBase.DB.playerData.vocal >= 47)
            noteVeloSet(6);
        else if (DataBase.DB.playerData.vocal >= 35 && DataBase.DB.playerData.vocal < 47)
            noteVeloSet(10);
        else if (DataBase.DB.playerData.vocal < 35)
            noteVeloSet(16);
        removeObjs();
    }
}
