using UnityEngine;
using SpaceGraphicsToolkit;
using System.Collections;

public class CloudCreator : MonoBehaviour
{
    private SgtCloudsphere cloudsphere;

    void Start()
    {
        cloudsphere = gameObject.GetComponent<SgtCloudsphere>();
    }

    public void CloudCreation()
    {
        StartCoroutine(Clouds());
    }

    private IEnumerator Clouds()
    {
        float CloudDensity = cloudsphere.Brightness;
        float count = 0.0f;
        while (count <= 1)
        {
            cloudsphere.Brightness = Mathf.Lerp(CloudDensity, 1.2f, count);
            count += Time.deltaTime;
            yield return null;
        }
    }
}