using UnityEngine;
using Lean.Touch;
using System.Collections;

public class DisableCameraZoom : MonoBehaviour
{
    private Camera cam;
    private LeanPinchCamera pinchCam;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(DisableZoom());
        }
    }

    IEnumerator DisableZoom()
    {
        cam = Camera.main;
        pinchCam = cam.GetComponent<LeanPinchCamera>();
        pinchCam.Zoom = 50;
        yield return new WaitForSeconds(1);
        pinchCam.enabled = false;
    }
}
