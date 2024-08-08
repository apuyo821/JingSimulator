using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject StandingImage;

    [SerializeField] Text txt_dialogue;
    [SerializeField] Text txt_name;

    [SerializeField] ScheduleManager scheduleManager;
    public Dialogue[] dialogues;
    SpriteManager sm;
    buttonManager buttonManager;

    bool isNext = false;    // 특정 키 입력 대기를 위한 변수
    public int dialogueCnt = 0;    // 대화 카운트. 한 캐릭터가 다 말하면 1 증가
    public int contextCnt = 0; 	// 대사 카운트. 한 캐릭터가 여러 대사를 할 수 있다.

    bool isDialogue = false;

    private void Start()
    {
        buttonManager = GameObject.Find("ButtonManager").gameObject.GetComponent<buttonManager>();
        sm = GetComponent<SpriteManager>();

        SettingUI(false);
    }

    private void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isNext = false;
                    txt_dialogue.text = "";

                    // 현재 캐릭터의 다음 대사 출력
                    if (++contextCnt < dialogues[dialogueCnt].contexts.Length)
                    {
                        TypeWriter();
                    }

                    // 다음 캐릭터의 대사 출력
                    else
                    {
                        contextCnt = 0;

                        if (++dialogueCnt < dialogues.Length)
                        {
                            TypeWriter();
                        }
                        // 다음 캐릭터가 없으면 (대화가 끝났으면)
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }

    void ChangeSprite()
    {
        // 캐릭터가 대사를 할 때, spriteName이 공백이 아니면 이미지 변경
        if (dialogues[dialogueCnt].spriteName[contextCnt] != "")
        {
            sm.SpriteChangeCoroutine(dialogues[dialogueCnt].tf_standing, dialogues[dialogueCnt].spriteName[contextCnt], dialogues[dialogueCnt].imageDirection);
        }
    }

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;

        txt_dialogue.text = "";
        txt_name.text = "";

        SettingUI(true);
        dialogues = p_dialogues;

        TypeWriter();
    }

    void SettingUI(bool p_flag)
    {
        dialoguePanel.SetActive(p_flag);
        StandingImage.SetActive(p_flag);
    }

    void EndDialogue()
    {
        isDialogue = false;
        contextCnt = 0;
        dialogueCnt = 0;
        dialogues = null;
        isNext = false;
        SettingUI(false);
        scheduleManager.isGO = true;
        scheduleManager.isEvent = false;
        buttonManager.trueBtnItr();
    }

    void TypeWriter()
    {
        SettingUI(true);    // 대사창 이미지를 띄운다.
        ChangeSprite();		// 스탠딩 이미지를 변경한다.

        string t_ReplaceText = dialogues[dialogueCnt].contexts[contextCnt];   // 특수문자를 ,로 치환
        t_ReplaceText = t_ReplaceText.Replace("'", ",");    // backtick을 comma로 변환
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n"); // 엑셀의 \n은 텍스트이기 때문에, 앞에 \를 한 번 더 입력

        txt_name.text = dialogues[dialogueCnt].name;
        txt_dialogue.text = t_ReplaceText;
        isNext = true;
    }    
}
