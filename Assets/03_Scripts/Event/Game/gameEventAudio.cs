using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEventAudio : MonoBehaviour
{
    [SerializeField] AudioSource[] gameAudios;

    private void Start()
    {
        foreach (AudioSource item in gameAudios)
        {
            item.volume = AudioManager.mainAudioVolume / 400;
        }
    }
}
