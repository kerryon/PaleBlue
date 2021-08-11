using UnityEngine;

public class MuteBtn : MonoBehaviour
{
    Animator muteAnimator;
    AudioSource audioSource;
    private bool isMuted;

    void Start()
    {
        isMuted = ES3.Load("MUTE", false);
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        muteAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMuted)
        {
            muteAnimator.SetBool("isMuted", isMuted);
        }
    }

    public void IsMuted()
    {
        isMuted = !isMuted;
        muteAnimator.SetBool("isMuted", isMuted);
        audioSource.mute = !audioSource.mute;
        ES3.Save("MUTE", isMuted);
    }
}
