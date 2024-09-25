using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data AudioClip", menuName = "Scriptable Objects AudioClip/AudioClipSO", order = 2)]
public class SOs_AudioClip : ScriptableObject
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private SOs_AudioMixer mixerSO;
    [Range(0, 1), SerializeField] private float volume = 1f;
    [Range(-3, 3), SerializeField] private float pitch = 1f;

    public AudioClip AudioClip
    {
        get { return audioClip; }
        set { }
    }
    public float Volume
    {
        get { return volume; }
        set { }
    }

    public float Pitch
    {
        get { return pitch; }
        set { }
    }
}
