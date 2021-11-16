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

    public void CreateCloud()
    {
        StartCoroutine(Clouds());
    }

    private IEnumerator Clouds()
    {
        Color cloudColorA = cloudsphere.Color;
        Color cloudColorB = cloudsphere.Color;
        cloudColorA.a = 0f;
        cloudColorB.a = 1f;

        float count = 0.0f;
        while (count <= 1)
        {
            cloudsphere.Color = Color.Lerp(cloudColorA, cloudColorB, count);
            count += Time.deltaTime;
            yield return null;
        }
    }
}