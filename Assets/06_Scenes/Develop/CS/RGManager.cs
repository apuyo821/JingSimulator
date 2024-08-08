using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Victory()
    {
        StartCoroutine(victory());
    }

    public void Fail()
    {
        StartCoroutine(fail());
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

    IEnumerator victory()
    {
        texts[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        texts[0].gameObject.SetActive(false);
    }

    IEnumerator fail()
    {
        texts[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        texts[1].gameObject.SetActive(false);
    }
}
