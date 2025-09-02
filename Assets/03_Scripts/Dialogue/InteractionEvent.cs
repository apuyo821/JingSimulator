using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public DialogueEvent dialogueEvent = new DialogueEvent();

    [SerializeField] Dialogue dummy_dialogue;


    // DatabaseManager에 저장된 실제 대사 데이터를 꺼내온다.
    public Dialogue[] GetDialogue()
    {
        //새로 만들어져서 아무것도 없는 dialogueEvent.dialogues에 더미의 tranform만 전달
        for (int j = 0; j < dialogueEvent.dialogues.Length; j++)
        {
            dialogueEvent.dialogues[j] = dummy_dialogue;
        }
        DialogueEvent t_dialogueEvent = new DialogueEvent();    // 임시 변수
        t_dialogueEvent.dialogues = DatabaseManager.instance.GetDialogue((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);

        for (int i = 0; i < dialogueEvent.dialogues.Length; i++)
        {
            // dialogueEvent에 넣은 Standing Image 오브젝트를 임시 변수에 넣기
            t_dialogueEvent.dialogues[i].tf_standing = dialogueEvent.dialogues[i].tf_standing;
        }

        // 원본에 임시 변수 덮어쓰기
        dialogueEvent.dialogues = t_dialogueEvent.dialogues;

        return dialogueEvent.dialogues;
    }
}
