using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gymEventManager : MonoBehaviour
{
    public int punchCount = 0;
    public bool isGO;
    int time = 5;

    [Header("UI")]
    public GameObject panel;
    public Text panelText;
    [SerializeField] GameObject scorePanel;
    [SerializeField] Text scroreText;

    public GameObject explainPanel;

    [SerializeField] GameObject homeButton;
    [SerializeField] TMP_Text timeCountText;
    [SerializeField] GameObject timeCountObject;

    [Header("사운드")]
    [SerializeField] AudioSource punchSound01;
    [SerializeField] AudioSource punchSound02;
    [SerializeField] AudioSource gameEvetnCountDownSound;
    [SerializeField] AudioSource boxingBellSoundEffect;

    [Header("징버거 캐릭터")]
    [SerializeField] GameObject jingObject;
    [SerializeField] SpriteRenderer jingSpriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float moveVelo;

    private void Start()
    {
        explainPanel.SetActive(true);
        homeButton.SetActive(false);
        jingObject.transform.localPosition = new Vector3(-300f, -150, 0);
        timeCountObject.SetActive(false);
        scorePanel.SetActive(false);
    }

    public void startTimer()
    {
        StartCoroutine(timer());
    }

    IEnumerator punchCounting()
    {
        while (isGO)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                punchCount++;
                punchSound01.Play();
            }
            yield return null;
        }
    }

    IEnumerator timer()
    {
        jingSpriteRenderer.sprite = sprites[0];
        panel.SetActive(true);
        panelText.text = "3";
        yield return new WaitForSeconds(1f);
        panelText.text = "2";
        yield return new WaitForSeconds(1f);
        panelText.text = "1";
        yield return new WaitForSeconds(1f);
        isGO = true;
        panel.SetActive(false);
        StartCoroutine(punchCounting());
        timeCountObject.SetActive(true);
        while (time != 0)
        {
            timeCountText.text = time.ToString();
            gameEvetnCountDownSound.Play();
            time--;
            if (time == 0)
                StartCoroutine(punch());
            yield return new WaitForSeconds(1f);
        }
        //StartCoroutine(punch());
        timeCountObject.SetActive(false);
        isGO = false;
        homeButton.SetActive(true);
        boxingBellSoundEffect.Play();
    }

    IEnumerator countCal()
    {
        scorePanel.SetActive(true);
        if (punchCount >= 31)
        {
            scroreText.text = "9";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "99";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "999";

        }
        else if(punchCount >= 21 && punchCount < 31)
        {
            scroreText.text = "9";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "90";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "900";

        }
        else if (punchCount >= 11 && punchCount < 20)
        {
            scroreText.text = "8";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "80";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "800";

        }
        else if (punchCount < 10)
        {
            scroreText.text = "7";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "70";
            yield return new WaitForSeconds(0.5f);
            scroreText.text = "700";
        }
    }

    IEnumerator punch()
    {
        Vector3 targetPosition = new Vector3(5, -150, 0);
        yield return new WaitForSeconds(1.7f);
        while (jingObject.transform.localPosition.x < 2)
        {
            jingObject.transform.localPosition = Vector3.Lerp(jingObject.transform.position, targetPosition, moveVelo);
            yield return null;
        }
        jingSpriteRenderer.sprite = sprites[1];
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(countCal());
    }
}
