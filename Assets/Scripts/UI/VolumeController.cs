using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private string _exposedParameter = "MasterVolume";
    public event Action VolumeChanged;
    private bool isInit = false;
    void Start()
    {
        SetPlayerPrefsVolume();
        isInit = true;
    }
    public void SetPlayerPrefsVolume()
    {
        _slider.value = PlayerPrefs.GetFloat(_exposedParameter, 0.60f);
    }
    public void ChangeVolume(float volumeSlider)
    {
        float volume = Mathf.Lerp(-40, 0, volumeSlider);
        if (volume <= -37) volume = -80;
        _audioMixer.SetFloat(_exposedParameter, volume);
        PlayerPrefs.SetFloat(_exposedParameter, volumeSlider);
        if (!isInit) return;
        VolumeChanged?.Invoke();
    }
}
