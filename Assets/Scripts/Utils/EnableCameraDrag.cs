using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class EnableCameraDrag : MonoBehaviour
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
