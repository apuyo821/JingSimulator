using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioPlay : MonoBehaviour
{
    public AudioSource mainAudio;

    private void Start()
    {
        mainAudio = GetComponent<AudioSource>();
        mainAudio.Play();
    }
}
