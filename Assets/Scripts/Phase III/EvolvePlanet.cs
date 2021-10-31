using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGraphicsToolkit;
using System;

public class EvolvePlanet : MonoBehaviour
{
    private SgtPlanet planet;
    private SgtPlanetWaterGradient planetWaterGradient;

    private SgtAtmosphere atmosphere;
    private SgtAtmosphereDepthTex atmosphereDepth;
    private SgtCloudsphere cloudsphere;
    private SgtCloudsphereDepthTex cloudsphereDepth;

    public Material[] materials;

    private bool changeMat = true;

    void Start()
    {
        planet = gameObject.GetComponent<SgtPlanet>();
        planetWaterGradient = gameObject.GetComponent<SgtPlanetWaterGradient>();

        atmosphere = gameObject.GetComponentInChildren<SgtAtmosphere>();
        atmosphereDepth = gameObject.GetComponentInChildren<SgtAtmosphereDepthTex>();
        cloudsphere = gameObject.GetComponentInChildren<SgtCloudsphere>();
        cloudsphereDepth = gameObject.GetComponentInChildren<SgtCloudsphereDepthTex>();
    }

    void Update()
    {
        if (planet.WaterLevel <= 0.45f)
        {
            planet.WaterLevel = (float)(Variables.Instance.timespan.TotalSeconds / 500f);
        }

        if (Variables.Instance.timespan.TotalSeconds > 20)
        {
            if (changeMat)
            {
                changeMat = false;
                StartCoroutine(ChangeOverTime(20));
                //planet.Material = materials[1]; //Material Lerp?????????
            }
            //planetMat.SetTexture("_MainTex", albedoMap);
            //planetMat.SetTexture("_BumpMap", normalMap);
            //planetMat.SetTexture("_HeightMap", heightMap);
        }
    }

    private IEnumerator ChangeOverTime(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            planetWaterGradient.Shallow = Color.Lerp(HexToColor("#602F1B"), HexToColor("#D2DFFF"), time/duration);
            planetWaterGradient.Deep = Color.Lerp(HexToColor("#E55721"), HexToColor("#5B7ACA"), time/duration);
            planet.Displacement = Mathf.Lerp(2.5f, 13f, time / duration);
            //materials[0].SetFloat("_WaterEmission", Mathf.Lerp(0.8f, 0, time/duration)); //notwendig, wenn darunter aktiv?
            planet.material.Lerp(materials[0], materials[1], time / duration);

            time += Time.deltaTime;
            yield return null;
        }
    }

    private Color HexToColor(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }
}
