using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioSource Music { get => musicSource; }
    public AudioSource SFX { get => sfxSource; }

    public static AudioManager Instance { get => instance; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
