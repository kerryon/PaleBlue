using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TogglePostProcessing : MonoBehaviour
{
    private Volume volume;
    private ChromaticAberration chAb;
    private ColorAdjustments coAd;
    private bool toggle = false;

    void Start()
    {
        volume = GameObject.Find("PostProcessing").GetComponent<Volume>();
        volume.profile.TryGet(out chAb);
        volume.profile.TryGet(out coAd);
    }

    public void TogglePP()
    {
        toggle = !toggle;
        chAb.active = toggle;
        coAd.active = toggle;
    }
}