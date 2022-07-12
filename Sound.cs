using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void SetSound(SoundDetial sound)
    {
        audioSource.clip = sound.audioClip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
    }
}
