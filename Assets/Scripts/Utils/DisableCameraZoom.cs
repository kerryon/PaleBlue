using UnityEngine;
using Lean.Touch;

public class DisableCameraZoom : MonoBehaviour
{
    private Camera cam;
    private LeanPinchCamera pitchCam;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            cam = Camera.main;
            pitchCam = cam.GetComponent<LeanPinchCamera>();
            pitchCam.enabled = false;
        }
    }
}
