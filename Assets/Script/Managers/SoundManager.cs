using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Music")]
    [SerializeField] private AudioClip musicClip;

    [Header("Sounds")]
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip fuelClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip unlockClip;
    [SerializeField] private AudioClip selectClip;
    [SerializeField] private AudioClip backClip;
    [SerializeField] private AudioClip goalClip;

    public AudioMixer audioMixer;
    public AudioClip CoinClip => coinClip;
    public AudioClip FuelClip => fuelClip;
    public AudioClip DeathClip => deathClip;
    public AudioClip UnlockClip => unlockClip;
    public AudioClip SelectClip => selectClip;
    public AudioClip BackClip => backClip;
    public AudioClip GoalClip => goalClip;




    private AudioSource musicAudioSource;
    private ObjectPooler soundObjectPooler;

    protected override void Awake()
    {
        soundObjectPooler = GetComponent<ObjectPooler>();
        musicAudioSource = GetComponent<AudioSource>();
        audioMixer = musicAudioSource.outputAudioMixerGroup.audioMixer;
        PlayMusic();
    }

    private void Start()
    {

        SetVolumeFromPlayerPrefs();
    }

    private void PlayMusic()
    {
        musicAudioSource.loop = true;
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlaySound(AudioClip clipToPlay, float volume = 1)
    {
        GameObject audioPooled = soundObjectPooler.GetObjectFromPool();
        AudioSource audioSource = null;

        if (audioPooled != null)
        {
            audioPooled.SetActive(true);
            audioSource = audioPooled.GetComponent<AudioSource>();
        }

        audioSource.clip = clipToPlay;
        audioSource.volume = volume;
        audioSource.Play();

        StartCoroutine(ReturnToPool(audioPooled, clipToPlay.length + 1));
    }

    private IEnumerator ReturnToPool(GameObject objectPool, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.SetActive(false);
    }

    public void PlaySelectSound()
    {
        PlaySound(SelectClip);
    }

    public void PlayBackSound()
    {
        PlaySound(BackClip);
    }

    private void SetVolumeFromPlayerPrefs()
    {
        audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Volume", 0));
    }

    public void SaveVolumeToPlayerPrefs()
    {
        float value;
        if(audioMixer.GetFloat("Master", out value))
            PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}
