using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    private void Start()
    {
        Parse("dialogueTxt");
    }

    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사 리스트
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);    // CSV 데이터

        string[] data = csvData.text.Split(new char[] { '\n' });    // 엔터 단위로 대사 쪼개기

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split( ',' );   // , 단위로 쪼개기

            Dialogue dialogue = new Dialogue(); // 캐릭터 한 명의 대사들

            dialogue.name = row[1];
            List<string> contextList = new List<string>();
            List<string> spriteList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);

                // 다음 줄 미리 비교
                if (++i < data.Length)
                {
                    row = data[i].Split(',');
                }

                // 다음 줄이 데이터 보다 길어지면 그냥 break
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");  // 다음 줄 캐릭터 이름이 공백이면 대사를 더 채우기

            // dialogue.context = row[2] ← context는 배열이기 때문에 이런 식으로 할당할 수 X.
            dialogue.contexts = contextList.ToArray();   // 리스트를 배열로
            dialogue.spriteName = spriteList.ToArray();
            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}