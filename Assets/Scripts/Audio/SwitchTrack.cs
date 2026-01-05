using UnityEngine;

public class SwitchTrack : MonoBehaviour
{
    public AudioClip clip;
    public bool playOnStart = false;
    private BackMusicController musicController;

    void Start()
    {
        musicController = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<BackMusicController>();
        if (playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        musicController.PlayTrack(clip);
    }

}
