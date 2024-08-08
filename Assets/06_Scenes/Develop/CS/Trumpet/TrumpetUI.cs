using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrumpetUI : MonoBehaviour
{
    public TrumpetJudge Judge;

    public Text comboTxt;
    public Text rankTxt;

    private void Awake()
    {
        setting_UI(false);
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
        else
        {
            setting_UI(true);
            rankTxt.text = Judge.rank.ToString();
            comboTxt.text = Judge.combo.ToString() + "X";
        }
    }

    void setting_UI(bool p_flag)
    {
        comboTxt.gameObject.SetActive(p_flag);
        rankTxt.gameObject.SetActive(p_flag);
    }
}
