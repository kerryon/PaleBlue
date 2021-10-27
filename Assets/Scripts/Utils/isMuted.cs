using UnityEngine;

public class isMuted : MonoBehaviour
{
    private AudioSource _as;
    private bool Muted;

    void Start()
    {
        Muted = ES3.Load("MUTE", false);
        if (Muted)
        {
            _as.mute = true;
        }
    }
}
