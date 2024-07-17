using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    public KeyCode KeyToPress;

    public Image image;
    public Sprite[] NoteType;
    public int typeNum;

    public int index;
    public static int pressNoteIndex=0;
    public int lastNoteIndex;
    
    public static List<GameObject> noteList = new List<GameObject>();

    /*
    private void Update()
    {
        TypeSetting();
        if (Input.GetKeyDown(KeyToPress))
        {
            if(index == pressNoteIndex)
            {
                Debug.Log(pressNoteIndex);
                noteList.Add(gameObject);
                image.enabled = false;
            }
            pressNoteIndex++;
            if (lastNoteIndex == pressNoteIndex)
            {
                pressNoteIndex = 0;
                Debug.Log("노트 다 침");
                for (int j = 0; j < noteList.Count; j++)
                {
                    noteList[j].GetComponent<Notes>().Invoke("remove", 0.1f);
                }
            }
        }
    }
    */

    public void OnEnable()
    {
        noteList.Add(gameObject);
    }

    public void startProcess()
    {
        StartCoroutine(process());
    }

    IEnumerator process()
    {
        while (true)
        {
            TypeSetting();
            yield return null;
            if (Input.GetKeyDown(KeyToPress))
            {
                if (index == pressNoteIndex)
                {
                    Debug.Log(pressNoteIndex);
                    image.enabled = false;
                    yield return null;
                    pressNoteIndex++;
                }
                if (lastNoteIndex == pressNoteIndex)
                {
                    pressNoteIndex = 0;
                    Debug.Log("노트 다 침");
                    RemoveProcess();
                }
            }
        }
    }

    public void TypeSetting()
    {
        switch (typeNum)
        {
            //Right Arrow
            case 0:
                image.sprite = NoteType[typeNum];
                KeyToPress = KeyCode.RightArrow;
                break;

            //Left Arrow
            case 1:
                image.sprite = NoteType[typeNum];
                KeyToPress = KeyCode.LeftArrow;
                break;

            //Up Arrow
            case 2:
                image.sprite = NoteType[typeNum];
                KeyToPress = KeyCode.UpArrow;
                break;

            //Down Arrow
            case 3:
                image.sprite = NoteType[typeNum];
                KeyToPress = KeyCode.DownArrow;
                break;

            default:
                break;

        }
    }

    public void RemoveProcess()
    {
        for (int j = 0; j < noteList.Count; j++)
        {
            noteList[j].GetComponent<Notes>().Invoke("remove", 0.1f);
        }
    }

    void remove()
    {
        for(int i =0; i<noteList.Count; i++)
        {
            noteList.RemoveAt(i);
        }
        Destroy(this.gameObject);
    }
}
