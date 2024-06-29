using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Speaker[] speakers;                        // 대화에 참여하는 캐릭터들의 UI 배열
    [SerializeField]
    private DialogData[] dialogs;                      // 현재 분기의 대사 목록 배열
    [SerializeField]
    private bool isAutoStart = true;                   // 자동 시작 여부
    private bool isFirst = true;                       // 최초 1회만 호출하기 위한 변수
    private int currentDialogSentenceIndex = -1;       // 현재 대사 순번
    private int currentSpeakerIndex = 0;               // 현재 말을 하는 화자(Speaker)의 speakers 배열 순번

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        // 모든 대화 관련 게임오브젝트 비활성화
        for (int i = 0; i < speakers.Length; ++i)
        {
            SetActiveObjects(speakers[i], false);
            // 캐릭터 이미지는 보이도록 설정
            speakers[i].speakerImage.gameObject.SetActive(true);
        }
    }

    public bool UpdateDialog()
    {
        // 대사 분기가 시작될 때 1회만 호출
        if (isFirst)
        {
            // 초기화. 캐릭터 이미지는 활성화하고, 대사 관련 UI는 모두 비활성화
            Setup();

            // 자동 재생(isAutoStart=true)으로 설정되어 있으면 첫 번째 대사 재생
            if (isAutoStart) SetNextDialog();

            isFirst = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // 대사가 남아있을 경우 다음 대사 진행
            if (dialogs.Length > currentDialogSentenceIndex + 1)
            {
                SetNextDialog();
            }
            // 대사가 더 이상 없을 경우 모든 오브젝트를 비활성화하고 true 반환
            else
            {
                // 현재 대화에 참여했던 모든 캐릭터, 대화 관련 UI를 보이지 않게 비활성화
                for (int i = 0; i < speakers.Length; ++i)
                {
                    SetActiveObjects(speakers[i], false);
                    // SetActiveObjects()에 캐릭터 이미지를 보이지 않게 하는 부분이 없기 때문에 별도로 호출
                    speakers[i].speakerImage.gameObject.SetActive(false);
                }
                return true;
            }
        }
        return false;
    }

    private void SetNextDialog()
    {
        // 이전 화자의 대화 관련 오브젝트 비활성화
        SetActiveObjects(speakers[currentSpeakerIndex], false);

        // 다음 대사를 진행하도록
        currentDialogSentenceIndex++;

        // 현재 화자 순번 설정
        currentSpeakerIndex = dialogs[currentDialogSentenceIndex].speakerIndex;

        // 현재 화자의 대화 관련 오브젝트 활성화
        SetActiveObjects(speakers[currentSpeakerIndex], true);
        // 현재 화자의 이름 텍스트 설정
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogSentenceIndex].name;
        // 현재 화자의 대사 텍스트 설정
        speakers[currentSpeakerIndex].textDialog.text = dialogs[currentDialogSentenceIndex].dialogSentence;
    }

    private void SetActiveObjects(Speaker speaker, bool visible)
    {
        speaker.imageDialog.gameObject.SetActive(visible);
        speaker.textName.gameObject.SetActive(visible);
        speaker.textDialog.gameObject.SetActive(visible);

        // 화살표는 대사가 종료되었을 때만 활성화하기 때문에 항상  false
        speaker.objectArrow.SetActive(false);

        // 캐릭터 알파 값 변경
        Color color = speaker.speakerImage.color;
        color.a = visible == true ? 1 : 0.2f;
        speaker.speakerImage.color = color;
    }
}

[System.Serializable]
public struct Speaker
{
    public Image speakerImage;             // 캐릭터 이미지
    public Image imageDialog;                         // 대화창 Image UI
    public Text textName;                             // 현재 대사중인 캐릭터 이름 출력 Text UI
    public Text textDialog;                           // 현재 대사 출력 Text UI
    public GameObject objectArrow;                    // 대사가 완료되었을 때 출력되는 커서 오브젝트
}

[System.Serializable]
public struct DialogData
{
    public int speakerIndex;          // 이름과 대사를 출력할 현재 DialogSystem의 Speaker 배열 순번
    public string name;               // 캐릭터 이름
    [TextArea(3, 5)]
    public string dialogSentence;     // 대사
}
