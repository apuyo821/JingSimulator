using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gymEventAudio : MonoBehaviour
{
    [SerializeField] AudioSource[] gymAudios;

    private void Start()
    {
        foreach (AudioSource item in gymAudios)
        {
            item.volume = AudioManager.sfxAudioVolume / 400;
        }
    }
}
