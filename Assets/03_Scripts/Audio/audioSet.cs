using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioSet : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource ddiYoungddiYoung;

    [Header("행동 효과음")]
    public AudioSource[] actSoundEffect;
    [Space(10f)]
    [SerializeField] OptionManager optionManager;

    private void Start()
    {
        clickSound = AudioManager.audioManager.sfx[6];
        ddiYoungddiYoung = AudioManager.audioManager.sfx[7];

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Start_option_menu")
        {
            foreach (AudioSource item in actSoundEffect)
            {
                item.volume = AudioManager.sfxAudioVolume / 100;
            }
        }
    }

    public void clickSoundPlay()
    {
        clickSound.Play();
    }

    public void ddiYoungddiYoungPlay()
    {
        ddiYoungddiYoung.Play();
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Start_option_menu")
        {
            foreach (AudioSource item in actSoundEffect)
            {
                item.volume = optionManager.volumeSlider[1].value / 150;
            }
        }
    }
}
