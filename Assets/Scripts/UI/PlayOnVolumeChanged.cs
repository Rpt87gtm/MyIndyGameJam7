using System.Collections;
using UnityEngine;

public class PlayOnVolumeChanged : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public float cooldown = 0.5f;
    private VolumeController _controller;

    private void Awake()
    {
        _controller = GetComponent<VolumeController>();
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnEnable()
    {
        _controller.VolumeChanged += PlaySound;
    }
    private void OnDisable()
    {
        _controller.VolumeChanged -= PlaySound;
    }
    private void PlaySound()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}
