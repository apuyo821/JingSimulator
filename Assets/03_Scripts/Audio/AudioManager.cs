using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [Header("Audios")]
    public AudioSource mainAudio;
    public AudioSource[] sfx; // 6 : clickSound
    public AudioSource TitleBgm;

    public static float mainAudioVolume;
    public static float sfxAudioVolume;

    private void Awake()
    {
        if (DataBase.dontDestroyObjects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }

        audioManager = this;

        DataBase.dontDestroyObjects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);

        mainAudio.volume = 0.7f;
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].volume = 0.7f;
        }
        sfx[6].volume = 0.2f;

        mainAudioVolume = 70f;
        sfxAudioVolume = 70f;
    }

    public void clickSoundPlay()
    {
        sfx[6].Play();
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(scene.name == "Title" || scene.name == "Start_option_menu")
        {
            if (!TitleBgm.isPlaying)
                TitleBgm.Play();
            else if (mainAudio.isPlaying)
                mainAudio.Stop();
        }
        else if(scene.name == "Main")
        {
            if (TitleBgm.isPlaying)
                TitleBgm.Stop();
            else if (!mainAudio.isPlaying)
                mainAudio.Play();
        }
        else
        {
            TitleBgm.Stop();
            mainAudio.Stop();
        }
    }
}
