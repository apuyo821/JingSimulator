using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public MainAudioPlay mainAudioCs;
    AudioSource mainAudioSource;

    public Slider volumeSlider;
    public Text volumeValueText;

    private void Start()
    {
        mainAudioSource = mainAudioCs.mainAudio.GetComponent<AudioSource>();
        volumeSlider.value = 70;
    }

    private void Update()
    {
        mainAudioSource.volume = volumeSlider.value / 100;
        volumeValueText.text = volumeSlider.value.ToString();
    }
}
