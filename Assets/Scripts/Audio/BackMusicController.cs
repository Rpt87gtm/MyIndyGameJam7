using System;
using System.Collections;
using UnityEngine;

public class BackMusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip defaultTrack;
    public float duration = 1f;
    private AudioClip currentTrack;
    private Coroutine trackSwitch;

    void Start()
    {
        if (defaultTrack != null)
        {
            PlayTrack(defaultTrack);
        }
    }

    public void PlayTrack(AudioClip newClip)
    {
        if (newClip == currentTrack)
        {
            Debug.Log("This clip is playing");
            return;
        }
        if (trackSwitch != null)
        {
            StopCoroutine(trackSwitch);
        }
        trackSwitch = StartCoroutine(SmoothChangeClip(newClip, duration));
    }

    private IEnumerator SmoothChangeClip(AudioClip newClip, float duration)
    {
        float currentTime = 0;
        if (currentTrack != null)
        {
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Math.Clamp(1 - currentTime / duration, 0, 1);
                yield return null;
            }
        }
        currentTrack = newClip;
        audioSource.clip = newClip;
        audioSource.Play();

        currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Math.Clamp(currentTime / duration, 0, 1);
            yield return null;
        }
    }
}
