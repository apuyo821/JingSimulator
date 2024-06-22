using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class DialogueParse : MonoBehaviour
{
    private static Dictionary<string, TalkData[]> TalkDictionary =
        new Dictionary<string, TalkData[]>();
    [SerializeField] string eventName;
    [SerializeField] TalkData[] talkDatas;
    [SerializeField] private TextAsset csvFile = null;

    private void Awake()
    {
        SetTalkDictionary();
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        return TalkDictionary[eventName];
    }

    public void SetTalkDictionary()
    {
        string csvText = csvFile.text.Substring(0, csvFile.text.Length - 1);
        string[] rows = csvText.Split(new char[] { '\n' });

        for (int i = 1; i < rows.Length; i++)
        {
            string[] rowValues = rows[i].Split(new char[] { ',' });

            if (rowValues[0].Trim() == "" || rowValues[0].Trim() == "end") continue;

            List<TalkData> talkDataList = new List<TalkData>();
            string eventName = rowValues[0];

            while (rowValues[0].Trim() != "end")
            {
                List<string> contextList = new List<string>();
                TalkData talkData;
                talkData.name = rowValues[1];

                do
                {
                    contextList.Add(rowValues[2].ToString());
                    if (++i < rows.Length)
                        rowValues = rows[i].Split(new char[] { ',' });
                    else break;
                } while (rowValues[1] == "" && rowValues[0] != "end");

                talkData.contexts = contextList.ToArray();
                talkDataList.Add(talkData);
            }
            TalkDictionary.Add(eventName, talkDataList.ToArray());
        }
    }
}