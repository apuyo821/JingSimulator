using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Dialogue
{
    [Tooltip("말하는 놈 이름")]
    public string name;

    [Tooltip("대사")]
    public string[] contexts;
}


[System.Serializable]
public class DialogueEvent
{
    public string name;

    public Vector2 Lines;
    public Dialogue[] dialogues;
}

public class test2 : MonoBehaviour
{
    Dialogue[] dialogues;
    List<Dialogue> dialogueList = new List<Dialogue>();
    string[] con = { "hi", "hello" };

    private void Start()
    {
        Dialogue dialogue;
        dialogue.name = "me";
        List<string> contextList = new List<string>();
        for(int i =0; i<con.Length; i++)
        {
            contextList.Add(con[i].ToString());
        }
        dialogue.contexts = contextList.ToArray();
    }
}