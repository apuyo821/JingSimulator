using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gymEventManager : MonoBehaviour
{
    public int punchCount = 0;
    public bool isGO;
    int time = 0;

    public GameObject panel;
    public Text panelText;

    public GameObject explainPanel;

    private void Start()
    {
        explainPanel.SetActive(true);
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
            }
            yield return null;
        }
    }

    IEnumerator timer()
    {
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
        while (time != 5)
        {
            time++;
            yield return new WaitForSeconds(1f);
        }
        isGO = false;
        countCal();
    }

    void countCal()
    {
        panel.SetActive(true);
        if (punchCount >= 31)
            panelText.text = "999";
        else if(punchCount >= 21 && punchCount < 31 )
            panelText.text = "900";
        else if (punchCount >= 11 && punchCount < 20)
            panelText.text = "800";
        else if (punchCount < 10)
            panelText.text = "700";
    }
}
