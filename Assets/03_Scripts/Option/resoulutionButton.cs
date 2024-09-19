using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resoulutionButton : MonoBehaviour
{
    [SerializeField] GameObject rBtn;
    [SerializeField] GameObject rPanel;

    [SerializeField] RectTransform rRect;
    [SerializeField] Toggle toggle;

    [SerializeField] TMP_Text tMP;

    private void Start()
    {
        rPanel.SetActive(false);
    }

    private void Update()
    {
        tMP.text = Screen.width + " X " + Screen.height;
    }

    public void set1920()
    {
        if (toggle.isOn)
            Screen.SetResolution(1920, 1080, true);
        else
            Screen.SetResolution(1920, 1080, false);

    }

    public void set1600()
    {
        if (toggle.isOn)
            Screen.SetResolution(1600, 900, true);
        else
            Screen.SetResolution(1600, 900, false);
    }

    public void set1366()
    {
        if (toggle.isOn)
            Screen.SetResolution(1366, 768, true);
        else
            Screen.SetResolution(1366, 768, false);
    }

    public void set1280()
    {
        if (toggle.isOn)
            Screen.SetResolution(1280, 720, true);
        else
            Screen.SetResolution(1280, 720, false);
    }

    public void fullscreen()
    {
        if (toggle.isOn)
            Screen.SetResolution(Screen.width, Screen.height, true);
        else
            Screen.SetResolution(Screen.width, Screen.height, false);
    }
}
