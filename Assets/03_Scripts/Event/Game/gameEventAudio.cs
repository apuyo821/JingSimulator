using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEventAudio : MonoBehaviour
{
    public AudioSource[] gameAudios;

    public AudioSource mainMusic;

    private void Start()
    {
        foreach (AudioSource item in gameAudios)
        {
            item.volume = AudioManager.sfxAudioVolume / 300;
        }
        mainMusic.volume = AudioManager.mainAudioVolume / 300;
    }
}
