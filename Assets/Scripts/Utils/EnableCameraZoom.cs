using Lean.Touch;
using UnityEngine;

public class EnableCameraZoom : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<LeanPinchCamera>().enabled = true;
            cam.GetComponent<LeanPinchCamera>().Zoom = 50;

        }
    }
}
