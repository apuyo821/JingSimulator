using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSet : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;

    private void Start()
    {
        clickSound = AudioManager.audioManager.sfx[6];
    }

    public void clickSoundPlay()
    {
        clickSound.Play();
    }
}
