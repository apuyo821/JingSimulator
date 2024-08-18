using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrumpetUI : MonoBehaviour
{
    public TrumpetJudge Judge;

    public Text comboTxt;
    public Text rankTxt;
    public Text resultTxt;
    public GameObject ResultPanel;

    bool isEnd;

    private void Awake()
    {
        setting_comboUI(false);
    }

    private void Update()
    {
        rankTxt.text = Judge.rank.ToString();
        if(Judge.combo == 0)
        {
            comboTxt.gameObject.SetActive(false);
            rankTxt.gameObject.SetActive(true);
            rankTxt.text = Judge.rank.ToString();
        }
        else if (isEnd)
        {
            setting_comboUI(false);
        }
        else
        {
            setting_comboUI(true);
            rankTxt.text = Judge.rank.ToString();
            comboTxt.text = Judge.combo.ToString() + "X";
        }
    }

    public void showResultPanel(string _resultRank)
    {
        isEnd = true;
        setting_comboUI(false);
        ResultPanel.SetActive(true);
        resultTxt.text = _resultRank;
    }

    void setting_comboUI(bool p_flag)
    {
        comboTxt.gameObject.SetActive(p_flag);
        rankTxt.gameObject.SetActive(p_flag);
    }
}
