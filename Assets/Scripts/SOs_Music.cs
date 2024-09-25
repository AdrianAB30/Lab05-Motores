using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data Music", menuName ="New Music Data")]
public class SOs_Music : ScriptableObject
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioClip audioClip;

}
