using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] Slider[] volumeSlider;
    [SerializeField] Text[] volumeValueText;
    [SerializeField] GameObject fullScreenCheck;

    private void Start()
    {
        volumeSlider[0].value = AudioManager.mainAudioVolume;
        volumeSlider[1].value = AudioManager.sfxAudioVolume;
    }

    private void Update()
    {
        AudioManager.mainAudioVolume = volumeSlider[0].value;
        AudioManager.sfxAudioVolume = volumeSlider[1].value;
        AudioManager.audioManager.mainAudio.volume = volumeSlider[0].value / 100;
        AudioManager.audioManager.TitleBgm.volume = volumeSlider[0].value / 100;
        volumeValueText[0].text = volumeSlider[0].value.ToString();

        for (int i = 0; i < AudioManager.audioManager.sfx.Length; i++)
        {
            AudioManager.audioManager.sfx[i].volume = volumeSlider[1].value / 100;
            AudioManager.audioManager.sfx[6].volume = volumeSlider[1].value / 1000 * 2;
        }
        volumeValueText[1].text = volumeSlider[1].value.ToString();

        if(transform.tag == "Title")
        {
            if (Screen.fullScreen)
                fullScreenCheck.SetActive(true);
            else
                fullScreenCheck.SetActive(false);
        }

    }
}
