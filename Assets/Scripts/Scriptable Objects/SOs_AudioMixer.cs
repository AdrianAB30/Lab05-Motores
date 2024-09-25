using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data Music", menuName = "Scriptable Objects Audio/AudioMixerSO", order = 1)]
public class SOs_AudioMixer : ScriptableObject
{
    [SerializeField] private AudioMixer MainMixer;
    [SerializeField] private AudioMixerGroup group;
    [SerializeField] private string volumeKey;
    [Range(-10f, 1)]
    [SerializeField] private float volumeValue;


    public void UpdateVolume(float value)
    {
        MainMixer.SetFloat(volumeKey, Mathf.Log10(value) * 20f);
    }
}
