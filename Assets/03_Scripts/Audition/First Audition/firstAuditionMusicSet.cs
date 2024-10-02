using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstAuditionMusicSet : MonoBehaviour
{
    [SerializeField] AudioSource mainMusic;

    private void Awake()
    {
        mainMusic.volume = AudioManager.mainAudioVolume / 125;
        mainMusic.Play();
    }
}
