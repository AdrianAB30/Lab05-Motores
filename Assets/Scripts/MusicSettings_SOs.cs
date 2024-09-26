using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Audio/AudioSettings")]
public class MusicSettings_SOs : ScriptableObject
{
    [SerializeField] private AudioClip menuMusicClip;
    public float menuMusicVolume = 1f;

    public AudioClip MenuMusicClip => menuMusicClip;
}
