using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RGManager : MonoBehaviour
{
    public static RGManager RGinstance;

    public GameObject[] Objs;
    public Timer timerCS;

    public Text[] texts;

    public int miss = 0;
    public bool clear = false;
    int noteAmount;
    int timeAmount = 3;

    private void Start()
    {
        RGinstance = this;
        timerCS = Objs[3].GetComponent<Timer>();
        StartCoroutine(TTO());
    }

    public void BeforeGameStart()
    {
        StartCoroutine(TTO());
    }

    public void StartGame()
    {
        timerCS.timerStart();
        timerCS.slider.value = 0;
        noteAmount = Random.Range(10,11);
        for(int i = 0; i<noteAmount; i++)
        {
            GameObject notes = Instantiate(Objs[0]);
            notes.transform.SetParent(Objs[1].transform);
            notes.transform.localScale = new Vector3(1f, 1f, 1f);
            Notes notesCS = notes.GetComponent<Notes>();
            notesCS.lastNoteIndex = noteAmount;
            notesCS.index = i;
            notesCS.typeNum = Random.Range(0, 4);
            notesCS.startProcess();
        }
    }

    public void winOrLose(int _or)
    {
        StartCoroutine(brawl(_or));
        Objs[4].SetActive(true);
    }

    public void GoMain()
    {
        DataBase.DB.playerData.auditionIndex++;
        SceneManager.LoadScene("Main");
    }

    IEnumerator TTO()
    {
        Objs[2].SetActive(true);
        while (timeAmount != 0)
        {
            texts[2].text = timeAmount.ToString();
            yield return new WaitForSeconds(1f);
            timeAmount--;
        }
        Objs[2].SetActive(false);
        yield return null;
        StartGame();
    }

    IEnumerator brawl(int _or)
    {
        texts[_or].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        texts[_or].gameObject.SetActive(false);
    }
}
