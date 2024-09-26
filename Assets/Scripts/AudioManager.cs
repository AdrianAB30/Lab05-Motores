using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private MusicSettings_SOs audioSettings;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        sfxSource = GetComponent<AudioSource>();
    }
    public void PlayMenuMusic()
    {
        if (audioSettings != null && audioSettings.MenuMusicClip != null)
        {
            musicSource.clip = audioSettings.MenuMusicClip;
            musicSource.volume = audioSettings.menuMusicVolume;
            musicSource.loop = true; 
            musicSource.Play();
        }
    }
}
