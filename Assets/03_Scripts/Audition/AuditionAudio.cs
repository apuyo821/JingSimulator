using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionAudio : MonoBehaviour
{
    [SerializeField] AudioSource[] AuditionAudiosMain;
    [SerializeField] AudioSource[] AuditionAudiosSFX;

    private void Start()
    {
        foreach (AudioSource item in AuditionAudiosSFX)
        {
            item.volume = AudioManager.sfxAudioVolume / 100;
        }

        foreach (AudioSource item in AuditionAudiosMain)
        {
            item.volume = AudioManager.mainAudioVolume / 100;
        }
    }
}
