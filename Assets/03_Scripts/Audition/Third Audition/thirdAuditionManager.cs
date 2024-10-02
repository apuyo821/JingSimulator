using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class thirdAuditionManager : MonoBehaviour
{
    [Header("메일 오브젝트")]
    [SerializeField] GameObject miniMail;
    [SerializeField] Button miniMailButton;
    [SerializeField] GameObject beforeShowResult;
    [SerializeField] GameObject afterShowResult;
    [SerializeField] TMP_Text resultText;

    [Space(10f)]
    [SerializeField] GameObject allResult;
    [SerializeField] GameObject goEndingButton;

    [Header("오디션 매니저")]
    [SerializeField] AuditionManager auditionManager;

    private void OnEnable()
    {
        Camera mainCamera = Camera.main;
        mainCamera.orthographic = true;

        allResult.SetActive(false);
        miniMail.SetActive(true);

        StartCoroutine(mailTransition());
        miniMailButton.interactable = false;
    }

    public void showReslut()
    {
        miniMail.SetActive(false);
        allResult.SetActive(true);
        StartCoroutine(showResultProcess());
    }

    public void GoEnding()
    {
        auditionManager.LastAudition();
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator showResultProcess()
    {
        beforeShowResult.SetActive(true);
        afterShowResult.SetActive(false);
        yield return new WaitForSeconds(0.75f);

        afterShowResult.SetActive(true);
        if (DataBase.DB.playerData.rizz + DataBase.DB.playerData.dance + DataBase.DB.playerData.vocal >= 250)
        {
            resultText.text = "합격!";
            DataBase.DB.thirdAudition = true;
        }
        else
        {
            resultText.text = "불합격";
            DataBase.DB.thirdAudition = false;
        }

        goEndingButton.SetActive(true);
    }

    IEnumerator mailTransition()
    {
        Vector3 targetPosition = new Vector3(650, 0, 0);
        yield return new WaitForSeconds(1f);
        miniMail.transform.localPosition = new Vector3(650, -330, 0);

        while (miniMail.transform.localPosition.y <= -1f)
        {
            miniMail.transform.localPosition = Vector3.Lerp(miniMail.transform.localPosition, targetPosition, 0.05f);
            yield return null;
        }

        miniMailButton.interactable = true;
    }
}
