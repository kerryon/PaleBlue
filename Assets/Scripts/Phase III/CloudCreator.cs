using UnityEngine;
using System.Collections;
using SpaceGraphicsToolkit.Cloudsphere;

public class CloudCreator : MonoBehaviour
{
    private SgtCloudsphere cloudsphere;
    private bool cloudNeedsCreation;
    private Color cloudColor;

    void Start()
    {
        cloudNeedsCreation = ES3.Load("CloudNeedsCreation", true);

        cloudsphere = gameObject.GetComponent<SgtCloudsphere>();
        cloudColor = cloudsphere.Color;
        if (cloudNeedsCreation)
        {
            cloudColor.a = 0f;
            cloudsphere.Color = cloudColor;
        }
        else
        {
            cloudColor.a = 1f;
            cloudsphere.Color = cloudColor;
        }
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