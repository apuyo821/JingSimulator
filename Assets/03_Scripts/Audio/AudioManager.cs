using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [Header("Audios")]
    public AudioSource mainAudio;
    public AudioSource[] sfx; // 0 : clickSound

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
    }
}
