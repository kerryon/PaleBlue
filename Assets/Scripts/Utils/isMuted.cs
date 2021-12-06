using UnityEngine;

public class IsMuted : MonoBehaviour
{
    private AudioSource _as;
    private bool Muted;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        Muted = ES3.Load("MUTE", false);
        if (Muted)
        {
            _as.mute = true;
        }
    }
}
