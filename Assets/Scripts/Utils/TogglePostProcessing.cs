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
        volume = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<Volume>();
        volume.profile.TryGet(out chAb);
        volume.profile.TryGet(out coAd);
    }

    public void TogglePP()
    {
        toggle = !toggle;
        chAb.active = toggle;
        coAd.active = toggle;
    }

    private void OnTriggerEnter(Collider other)
    {
        chAb.active = true;
        coAd.active = true;
    }

    private void OnTriggerExit(Collider other)
    {
        chAb.active = false;
        coAd.active = false;
    }
}