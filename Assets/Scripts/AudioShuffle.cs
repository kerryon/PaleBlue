using UnityEngine;

public class AudioShuffle : MonoBehaviour
{
    private AudioSource _as;
    public AudioClip[] audioClipArray;

    private bool isMuted;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        if (audioClipArray.Length > 5)
        {
            PlayStartAudioClip();
        } else
        {
            PlayRandomAudioClip();
        }

        isMuted = ES3.Load("MUTE", false);
        if (isMuted)
        {
            _as.mute = true;
        }
    }

    void Update()
    {
        if (!_as.isPlaying)
        {
            PlayRandomAudioClip();
        }
    }

    void PlayRandomAudioClip()
    {
        _as.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        _as.Play();
    }

    void PlayStartAudioClip()
    {
        _as.clip = audioClipArray[0];
        _as.Play();
    }
}