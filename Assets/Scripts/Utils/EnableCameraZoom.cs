using Lean.Touch;
using UnityEngine;

public class EnableCameraZoom : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            cam = GameObject.FindGameObjectWithTag("CameraPivot");
            cam.GetComponent<LeanMultiUpdate>().enabled = true;
        }
    }
}
