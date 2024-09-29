using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameEventManager : MonoBehaviour
{
    [SerializeField] GameObject itrObj;
    [SerializeField] InteractionEvent itrCs;
    [SerializeField] DialogueManager dmCs;
    [SerializeField] GameObject[] cutScens;
    [SerializeField] AudioSource[] cutSceneAudios;

    int dialoguLength;

    [Header("대사")]
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject dialogueObject;

    [Header("버튼")]
    [SerializeField] GameObject goHomeButton;


    private void Start()
    {
        goHomeButton.SetActive(false);
        StartCoroutine(dialogProcesss());
    }

    IEnumerator dialogProcesss()
    {
        cutSceneAudios[0].Play();
        dialogueObject.SetActive(true);
        cutScens[0].SetActive(true);
        dialogueText.text = "20XX 서든어택 클랜전 결승 시자아아아아아";
        yield return new WaitForSeconds(4.5f);

        cutScens[0].SetActive(false);
        cutScens[1].SetActive(true);
        dialogueText.text = "아아아악 하겠습니다!!!!!!";

        yield return new WaitUntil(() => cutSceneAudios[0].isPlaying == false);
        dialogueObject.SetActive(false);

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

        itrCs.dialogueEvent.line.x = 38;
        itrCs.dialogueEvent.line.y = 38;
        dialoguLength = 1;
        itrCs.dialogueEvent.name = "게임 이벤트 끝";
        itrCs.dialogueEvent.dialogues = new Dialogue[dialoguLength];
        dmCs.ShowDialogue(itrCs.GetDialogue());

        yield return new WaitForSeconds(2f);
        goHomeButton.SetActive(true);
    }

    public void goHome()
    {
        SceneManager.LoadScene("Main");
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
