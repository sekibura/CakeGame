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
        SaveManager.Instance.Save<float>("MasterVolume", enable? 0:-80);
    }


    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        float volume = SaveManager.Instance.Load<float>("MasterVolume");
        _mixer.audioMixer.SetFloat("MasterVolume", volume);
    }

    
    public void Init()
    {
        //init volume
        float volume = SaveManager.Instance.Load<float>("MasterVolume");
        _mixer.audioMixer.SetFloat("MasterVolume", volume);
        //_mixer.audioMixer.GetFloat("MasterVolume", out volume);
        if (volume == 0)
            _audioToggle.isOn = true;
        else if (volume == -80)
            _audioToggle.isOn = false;

        //init vibro
    }

    public void SetVolumeLerp(float value)
    {
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));
        SaveManager.Instance.Save<float>("MasterVolume", value);
    }

    public void SetVolume(float value)
    {
        _mixer.audioMixer.SetFloat("MasterVolume",  value);
        SaveManager.Instance.Save<float>("MasterVolume", value);

    }
    public void LoadVolume()
    {
        float volume = SaveManager.Instance.Load<float>("MasterVolume");
        Debug.Log("Loaded volume: "+volume);
        SetVolume(volume);
    }

    public float GetVolume()
    {
        float volume;
        _mixer.audioMixer.GetFloat("MasterVolume", out volume);
        return volume;
    }

    public void ToggleVibration(bool value)
    {

    }
}
