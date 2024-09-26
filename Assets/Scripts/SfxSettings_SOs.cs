using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXAudioSettings", menuName = "Audio/SFXAudioSettings")]
public class SfxSettings_SOs : ScriptableObject
{
    [SerializeField] private AudioClip clickSFXClip;
    public float volumeSfx = 1f;
    public AudioClip ClickSFXClip => clickSFXClip;
}
