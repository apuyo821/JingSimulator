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

    Timer timerCS;

    string resultRank;

    public void OnEnable()
    {
        noteList.Add(gameObject);
        GameObject timerObj = GameObject.Find("Timer");
        timerCS = timerObj.GetComponent<Timer>();
    }

    public void startProcess()
    {
        StartCoroutine(process());
    }

    IEnumerator process()
    {
        while (true)
        {
            //여기는 건드는거 아님
            TypeSetting();
            yield return null;
            //여기는 건드는거 아님
            

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(index == pressNoteIndex)
                {
                    if (Input.GetKeyDown(KeyToPress))
                    {
                        image.enabled = false;
                        yield return null;
                        pressNoteIndex++;
                    }
                    else
                    {
                        for(int k = 0; k<noteList.Count; k++)
                        {
                            noteList[k].GetComponent<Image>().color = new Color32(225, 105, 105, 150);
                        }
                        yield return new WaitForSeconds(0.25f);
                        for (int k = 0; k < noteList.Count; k++)
                        {
                            noteList[k].GetComponent<Image>().enabled = true;
                            noteList[k].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                        }
                        pressNoteIndex = 0;
                        RGManager.RGinstance.miss++;
                    }
                    if (lastNoteIndex == pressNoteIndex)
                    {
                        pressNoteIndex = 0;
                        RGManager.RGinstance.clear = true;
                        timerCS.timeStop();
                        RGManager.RGinstance.winOrLose(0);  //success
                        ResultCalculate(RGManager.RGinstance.miss, RGManager.RGinstance.clear);
                        RemoveProcess();
                    }
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

    void ResultCalculate(int _miss, bool _clear)
    {
        if (_clear == true || _miss == 0)
            resultRank = "S";
        else if (_clear == true || _miss > 0 || _miss < 3)
            resultRank = "A";
        else if (_clear == true || _miss >= 3)
            resultRank = "F";

        RGManager.RGinstance.texts[3].text = resultRank;
    }
}
