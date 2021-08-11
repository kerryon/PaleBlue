using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGraphicsToolkit;

public class CloudCreator : MonoBehaviour
{
    private SgtCloudsphere cloudsphere;
    void Start()
    {
        cloudsphere = gameObject.GetComponent<SgtCloudsphere>();
    }

    public void CloudCreation()
    {
        cloudsphere.Brightness = 1;
    }

    void Update()
    {
        
    }
}
