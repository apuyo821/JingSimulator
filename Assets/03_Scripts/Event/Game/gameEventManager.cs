using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEventManager : MonoBehaviour
{
    [SerializeField] GameObject itrObj;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;

    int dialoguLength;
    public static bool isEnd = false;

    private void Start()
    {
        StartCoroutine(dialogProcesss());
    }

    public void dialogProcess()
    {
        itrCs.dialogueEvent.line.x = 7;
        itrCs.dialogueEvent.line.y = 9;
        dialoguLength = 3;
        itrCs.dialogueEvent.name = "게임 이벤트";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());
    }

    IEnumerator dialogProcesss()
    {
        isEnd = false;
        itrCs.dialogueEvent.line.x = 7;
        itrCs.dialogueEvent.line.y = 7;
        dialoguLength = 1;
        itrCs.dialogueEvent.name = "게임 이벤트 시작";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());

        yield return new WaitUntil(() => isEnd == true);
        yield return new WaitForSeconds(2f);

        isEnd = false;
        itrCs.dialogueEvent.line.x = 8;
        itrCs.dialogueEvent.line.y = 9;
        dialoguLength = 2;
        itrCs.dialogueEvent.name = "게임 이벤트 끝";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());
    }
}
