using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEventManager : MonoBehaviour
{
    [SerializeField] GameObject itrObj;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;
    [SerializeField] GameObject[] cutScens;
    [SerializeField] AudioSource[] cutSceneAudios;

    int dialoguLength;
    bool isEnd = false;

    private void Start()
    {
        StartCoroutine(dialogProcesss());
    }

    IEnumerator dialogProcesss()
    {
        isEnd = false;
        itrCs.dialogueEvent.line.x = 38;
        itrCs.dialogueEvent.line.y = 38;
        dialoguLength = 1;
        itrCs.dialogueEvent.name = "게임 이벤트 시작";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());

        //시자ㅏㅏㅏㅏㅏㅏㅏㅏㅏ악합니다
        cutScens[0].SetActive(true);
        cutSceneAudios[0].Play();
        yield return new WaitForSeconds(4.5f);
        cutScens[0].SetActive(false);
        cutScens[1].SetActive(true);

        yield return new WaitUntil(() => cutSceneAudios[0].isPlaying == false);
        //성태님 나이서
        cutScens[1].SetActive(false);
        cutScens[2].SetActive(true);
        cutSceneAudios[1].Play();

        yield return new WaitUntil(() => cutSceneAudios[1].isPlaying == false);
        //클러치 장면
        cutScens[2].SetActive(false);
        cutScens[3].SetActive(true);
        cutSceneAudios[2].Play();
        yield return new WaitForSeconds(6f);
        cutScens[3].SetActive(false);
        cutScens[4].SetActive(true);

        yield return new WaitUntil(() => cutSceneAudios[2].isPlaying == false);



        isEnd = false;
        itrCs.dialogueEvent.line.x = 39;
        itrCs.dialogueEvent.line.y = 39;
        dialoguLength = 1;
        itrCs.dialogueEvent.name = "게임 이벤트 끝";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());
    }
}
