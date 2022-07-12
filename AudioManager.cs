using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [SerializeField] SoundData_SO soundData_SO;
    [SerializeField] SceneSoundData_SO sceneSoundData_SO;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerSnapshot normalSnapshot;
    [SerializeField] AudioMixerSnapshot ambientOnlySnapshot;
    [SerializeField] AudioMixerSnapshot muteSnapshot;
    [SerializeField] float musicStartTime = 2f;
    AudioSource gameMusicSource;
    AudioSource ambientSource;
    Coroutine soundRoutine;
    private void OnEnable()
    {
        EventHandler.PlaySoundEvent += OnPlaySoundEvent;
    }
    private void OnDisable()
    {
        EventHandler.PlaySoundEvent -= OnPlaySoundEvent;
    }
    void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetials = soundData_SO.GetSoundDetial(soundName);
        EventHandler.CallInitSoundEffects(soundDetials);
    }
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        var soundDetials = sceneSoundData_SO.GetSceneSoundDetial(currentScene);
        var gameMusicData = soundData_SO.GetSoundDetial(soundDetials.musicName);
        var ambientData = soundData_SO.GetSoundDetial(soundDetials.ambientName);
        if (soundRoutine != null)
            StopCoroutine(soundRoutine);
        soundRoutine = StartCoroutine(PlayBGMRoutian(gameMusicData,ambientData));
    }
    void PlayGameMusic(SoundDetial soundDetial, float duration)
    {
        audioMixer.SetFloat("MusicVolume", Conveter(soundDetial.volume));
        gameMusicSource.clip = soundDetial.audioClip;
        if(gameMusicSource.isActiveAndEnabled)
        {
            gameMusicSource.Play();
        }
        normalSnapshot.TransitionTo(duration);
    }
    void PlayAmbientMusic(SoundDetial soundDetial, float duration)
    {
        audioMixer.SetFloat("AmbientVolume", Conveter(soundDetial.volume));
       ambientSource.clip = soundDetial.audioClip;
        if (ambientSource.isActiveAndEnabled)
        {
            ambientSource.Play();
        }
        ambientOnlySnapshot.TransitionTo(duration);
    }
    IEnumerator PlayBGMRoutian(SoundDetial gameMusicDetials, SoundDetial ambientDetials)
    {
        PlayAmbientMusic(ambientDetials, 1f);
        yield return new WaitForSeconds(musicStartTime);
        PlayGameMusic(gameMusicDetials, 1f);
    }
    float Conveter(float amount)
    {
        return (amount * 100 - 80);
    }
}
