using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSet : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource ddiYoungddiYoung;

    private void Start()
    {
        clickSound = AudioManager.audioManager.sfx[6];
        ddiYoungddiYoung = AudioManager.audioManager.sfx[7];
    }

    public void clickSoundPlay()
    {
        clickSound.Play();
    }

    public void ddiYoungddiYoungPlay()
    {
        ddiYoungddiYoung.Play();
    }
}
