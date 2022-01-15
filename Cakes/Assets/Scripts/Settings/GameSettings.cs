using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _mixer;

    [SerializeField]
    private Toggle _audioToggle;
    [SerializeField]
    private Toggle _vibroToggle;

    public void ToggleMusic(bool enable)
    {
        if(enable)
        _mixer.audioMixer.SetFloat("MasterVolume", 0);
        else
            _mixer.audioMixer.SetFloat("MasterVolume", -80);
    }

    

    public void Init()
    {
        //init volume
        float volume = 0;
        _mixer.audioMixer.GetFloat("MasterVolume", out volume);
        if (volume == 0)
            _audioToggle.isOn = true;
        else if (volume == -80)
            _audioToggle.isOn = false;

        //init vibro
    }

    public void SetVolume(float value)
    {
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
    }

    public void ToggleVibration(bool value)
    {

    }
}
