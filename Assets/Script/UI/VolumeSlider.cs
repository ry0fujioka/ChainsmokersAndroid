using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetAudioMixerBGM);
    }

    private void OnEnable()
    {
        float value;
        if (SoundManager.Instance.audioMixer.GetFloat("Master", out value))
        {
            if (value <= -20)
                value = -20;
            if (value > 0)
                value = 0;
            volumeSlider.value = value;
        }
        else
            volumeSlider.value = 0;
    }

    public void SetAudioMixerBGM(float value)
    {
        if (value <= -20)
            value = -80;
        SoundManager.Instance.audioMixer.SetFloat("Master", value);
        SoundManager.Instance.SaveVolumeToPlayerPrefs();
    }
}
