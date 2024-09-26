using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private SfxSettings_SOs sfxAudioSettings;
    private AudioSource sfxSource;

    private void Awake()
    {
        sfxSource = GetComponent<AudioSource>();
    }
    public void PlayButtonClickSFX()
    {
        if (sfxAudioSettings != null && sfxAudioSettings.ClickSFXClip != null)
        {
            sfxSource.clip = sfxAudioSettings.ClickSFXClip;
            sfxSource.volume = sfxAudioSettings.volumeSfx;
            sfxSource.PlayOneShot(sfxAudioSettings.ClickSFXClip); 
        }
    }
}
