using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGManager : MonoBehaviour
{
    public GameObject Note;
    public GameObject bar;

    public GameObject Timer;
    public Timer timerCS;

    int noteAmount;

    private void Start()
    {
        timerCS = Timer.GetComponent<Timer>();
    }

    public void StartGame()
    {
        timerCS.timerStart();
        timerCS.slider.value = 0;
        noteAmount = Random.Range(4,7);
        for(int i = 0; i<noteAmount; i++)
        {
            GameObject notes = Instantiate(Note);
            notes.transform.SetParent(bar.transform);
            notes.transform.localScale = new Vector3(1f, 1f, 1f);
            Notes notesCS = notes.GetComponent<Notes>();
            notesCS.lastNoteIndex = noteAmount;
            notesCS.index = i;
            notesCS.typeNum = Random.Range(0, 4);
            notesCS.startProcess();
        }
    }
}
