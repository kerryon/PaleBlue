using System.Collections;
using Lean.Touch;
using UnityEngine;

public class EnableCameraZoom : MonoBehaviour
{
    private GameObject cam;

    void Start()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(EnableZoom());
        }
    }

    IEnumerator EnableZoom()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        yield return new WaitForSeconds(1.1f);
        cam.GetComponent<LeanPinchCamera>().enabled = true;
    }
}
