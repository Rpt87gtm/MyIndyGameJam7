using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoundList", menuName = "Audio/RandomSound")]
public class RandomSoundList : ScriptableObject
{
    public List<AudioClip> audioClips;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    public AudioClip GetRandomSound()
    {
        if (audioClips == null || audioClips.Count == 0)
        {
            Debug.LogWarning("AudioClips list is empty or null!");
            return null;
        }

        int randomIndex = Random.Range(0, audioClips.Count);
        return audioClips[randomIndex];
    }

    public float GetRandomPitch()
    {
        if (minPitch > maxPitch)
        {
            Debug.LogWarning("minPitch is greater than maxPitch! Swapping values.");
            float temp = minPitch;
            minPitch = maxPitch;
            maxPitch = temp;
        }

        return Random.Range(minPitch, maxPitch);
    }
    public (AudioClip clip, float pitch) GetRandomSoundWithPitch()
    {
        return (GetRandomSound(), GetRandomPitch());
    }
    public void PlayRandomSound(AudioSource audioSource)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null!");
            return;
        }

        AudioClip clip = GetRandomSound();
        if (clip != null)
        {
            audioSource.pitch = GetRandomPitch();
            audioSource.PlayOneShot(clip);
        }
    }
}
