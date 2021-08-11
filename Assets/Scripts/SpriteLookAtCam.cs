using UnityEngine;

public class SpriteLookAtCam : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
