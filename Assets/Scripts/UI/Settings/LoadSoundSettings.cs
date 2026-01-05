using UnityEngine;
using UnityEngine.Audio;

public class LoadSoundSettings : MonoBehaviour
{
    public AudioMixer _audioMixer;
    private void Start()
    {
        LoadParam("masterVolume");
        LoadParam("musicVolume");
        LoadParam("effectsVolume");
    }
    public void LoadParam(string parameter)
    {
        ChangeVolume(parameter, PlayerPrefs.GetFloat(parameter, 0.60f));
    }
    public void ChangeVolume(string parameter, float volumeSlider)
    {
        float volume = Mathf.Lerp(-40, 0, volumeSlider);
        if (volume <= -37) volume = -80;
        _audioMixer.SetFloat(parameter, volume);
    }
}
