using UnityEngine;
using SpaceGraphicsToolkit;

public class CloudDensity : MonoBehaviour
{
    SgtCloudsphere _clouds;

    void Start()
    {
        _clouds = gameObject.GetComponent<SgtCloudsphere>();
        InvokeRepeating(nameof(SetCloudDensity), 0f, 1f);
    }

    private void SetCloudDensity()
    {
        _clouds.Brightness = Mathf.Lerp(0.3f, 1f, Variables.Instance.rain / 300f);
    }
}