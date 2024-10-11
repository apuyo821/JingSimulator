using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class gameEventManager : MonoBehaviour
{
    [SerializeField] gameEventAudio gEAudio;

    [SerializeField] GameObject volumeObj;
    [SerializeField] Volume volume;
    [SerializeField] DepthOfField depth;
    [SerializeField] Bloom bloom;

    [Header("컷신과 대사")]
    [SerializeField] GameObject cutScenes;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text context;
    [SerializeField] string firstDialogue = "이 폭탄만 해체한다면....";
    [SerializeField] float textDelay;
    [SerializeField] Button dialogueEndBtn;


    [Header("해체 게이지")]
    [SerializeField] GameObject boomSliderObj;
    [SerializeField] Slider boomSlider;
    bool isDonwKey = false;

    [SerializeField] bool isDialoguEnd = false;

    [Header("마지막 대사와 일러스트")]
    [SerializeField] Image backGround;
    [SerializeField] TMP_Text lastContext;
    [SerializeField] GameObject lastContextObj;
    [SerializeField] string lastDialogue = "울산징버거가 또다시 해냅니다";
    [SerializeField] GameObject Illust;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] Image imageChangeWhite;


    [Header("버튼")]
    [SerializeField] GameObject goHomeButton;

    private void Awake()
    {
        volumeObj.SetActive(true);
    }

    private void Start()
    {
        //필요한 컴포넌트들 얻기
        volume = volumeObj.gameObject.GetComponent<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out depth);

        isDialoguEnd = false;
        goHomeButton.SetActive(false);
        StartCoroutine(gEProcess());
        bloom.active = false;
        boomSliderObj.SetActive(false);
    }

    IEnumerator gEProcess()
    {
        //상황 설명 대사와 컷신 오픈
        cutScenes.SetActive(true);
        dialoguePanel.SetActive(true);
        context.text = "";
        dialogueEndBtn.interactable = false;

        //상황 설명 대사 출력
        for (int i = 0; i < firstDialogue.Length; i++)
        {
            context.text += firstDialogue[i];
            yield return new WaitForSeconds(textDelay);
        }
        dialogueEndBtn.interactable = true;

        yield return new WaitUntil(() => isDialoguEnd == true);

        //폭탄 해체 시작
        boomSliderObj.SetActive(true);
        boomSlider.value = 0;
        StartCoroutine(stopPlusGaze());
        while (boomSlider.value != 4)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(isDonwKey == false)
                {
                    isDonwKey = true;
                    gEAudio.gameAudios[0].Play();
                }
                boomSlider.value += 0.075f;
            }
            yield return new WaitForSeconds(0.065f);
        }

        boomSliderObj.SetActive(false);

        //화면 어두워지기
        depth.active = false;
        bloom.active = true;
        backGround.gameObject.SetActive(true);
        Color color = new Color(0, 0, 0, 0);
        while (backGround.color.a <= 1f)
        {
            backGround.color = color;
            color.a += 0.1f;
            yield return null;
        }

        gEAudio.mainMusic.Play();
        yield return new WaitForSeconds(2f);

        //감성 터지는 대사 출력
        lastContextObj.SetActive(true);
        lastContext.text = "";
        for (int i = 0; i < lastDialogue.Length; i++)
        {
            lastContext.text += lastDialogue[i];
            yield return new WaitForSeconds(textDelay + 0.1f);
        }

        color = new Color(255, 255, 255, 1f);

        yield return new WaitForSeconds(1.5f);
        while (lastContext.color.a >= 0)
        {
            lastContext.color = color;
            color.a -= 0.025f;
            yield return null;
        }
        lastContextObj.SetActive(false);

        StartCoroutine(downMainMusicVolume());
        yield return new WaitForSeconds(1.5f);

        //어두운거 풀리면서 일러스트 보임
        gEAudio.gameAudios[1].volume = gEAudio.gameAudios[1].volume / 2;
        gEAudio.gameAudios[1].Play();
        yield return new WaitForSeconds(1f);
        bloom.intensity.value = 0.5f;
        Illust.SetActive(true);
        StartCoroutine(whiteEffect());
        Color moreBright = new Color(0, 0, 0, 1f);
        while (backGround.color.a >= 0f)
        {
            backGround.color = moreBright;
            moreBright.a -= 0.025f;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        goHomeButton.SetActive(true);
    }

    IEnumerator stopPlusGaze()
    {
        while (boomSlider.value != 4)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isDonwKey == true)
                {
                    isDonwKey = false;
                    gEAudio.gameAudios[0].Stop();
                }
                boomSlider.value = 0;
            }

            yield return null;
        }
    }

    IEnumerator downMainMusicVolume()
    {
        for (int i = 0; i < 4; i++)
        {
            gEAudio.mainMusic.volume = gEAudio.mainMusic.volume / 1.15f;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator whiteEffect()
    {
        imageChangeWhite.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        while (imageChangeWhite.color.a >= 0f)
        {
            Color moreBright = new Color(255, 255, 255, 1f);
            while (imageChangeWhite.color.a >= 0f)
            {
                imageChangeWhite.color = moreBright;
                moreBright.a -= 0.025f;
                yield return null;
            }
        }
    }


    public void dialogueEnd()
    {
        isDialoguEnd = true;
    }


    public void goHome()
    {
        rewardPanel.SetActive(true);
        DataBase.DB.playerData.money += 500;
        Invoke("goHomeInvoke", 2f);
    }

    void goHomeInvoke()
    {
        SceneManager.LoadScene("Main");
    }

    public void goTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
