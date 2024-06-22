using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TalkData
{
    public string name; // 대사 치는 캐릭터 이름
    public string[] contexts; // 대사 내용
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] string eventName = null;
    [SerializeField] TalkData[] talkDatas = null;

    public TalkData[] GetObjectDialogue()
    {
        return DialogueParse.GetDialogue(eventName);
    }
}
