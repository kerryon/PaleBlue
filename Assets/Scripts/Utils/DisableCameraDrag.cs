using UnityEngine;
using Lean.Touch;
using Lean.Common;

public class DisableCameraDrag : MonoBehaviour
{
    private GameObject cam;
    private LeanPitchYaw yaw;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            cam = GameObject.FindGameObjectWithTag("CameraPivot");
            yaw = cam.GetComponent<LeanPitchYaw>();
            yaw.Pitch = 0;
            yaw.Yaw = 0;
            cam.transform.rotation = new Quaternion(0, 0, 0, 0);
            cam.GetComponent<LeanMultiUpdate>().enabled = false;

        }
    }
}
