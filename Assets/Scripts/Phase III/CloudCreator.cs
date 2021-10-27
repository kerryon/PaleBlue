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
        float count = 0.01f;
        while (count <= 1)
        {
            cloudsphere.Brightness = count;
            yield return new WaitForSeconds(0.05f);
            count += 0.1f;
        }
        yield return null;
    }

    public void StartWorldBreeding()
    {
        //in Echtzeit Pins + Popups auf den Planeten setzen
    }
}