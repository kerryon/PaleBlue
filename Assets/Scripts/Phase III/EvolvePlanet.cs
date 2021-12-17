using System.Collections;
using UnityEngine;
using SpaceGraphicsToolkit;

public class EvolvePlanet : MonoBehaviour
{
    private SgtPlanet planet;
    private SgtPlanetWaterGradient planetWaterGradient;

    private SgtAtmosphere atmosphere;
    private SgtAtmosphereDepthTex atmosphereDepth;
    private SgtCloudsphere cloudsphere;
    private SgtCloudsphereDepthTex cloudsphereDepth;

    [Header("Wait in Realtime")]
    public int Minutes;

    public Material[] materials;

    private bool changeMat = true;
    private bool changeNextMat = true;

    [Header("New Planet Variables for Materials", order = 1)]
    [Space(10)]
    [Header("SgtPlanet", order = 2)]
    public float[] _Displacement;
    public float[] _WaterLevel;
    [Header("SgtPlanetWaterGradient")]
    public Color[] _Shallow;
    public Color[] _Deep;
    public float[] _Sharpness;
    public float[] _Scale;
    [Header("SgtCloudsphere")]
    public Color[] _CColor;
    public Color[] _AmbientColor;
    public float[] _Radius;
    [Header("SgtCloudsphereDepthTex")]
    public Color[] _RimColor;
    [Header("SgtAtmopshere")]
    public Color[] _AColor;
    [Header("SgtAtmosphereDepthTex")]
    public Color[] _HorizonColor;
    public Color[] _InnerColor;
    public Color[] _OuterColor;

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
        if (Variables.Instance.timespan.TotalMinutes < Minutes)
        {
            planet.WaterLevel = Mathf.Lerp(0.0f, 0.24f, (float)(Variables.Instance.timespan.TotalSeconds / (Minutes * 60)));
        }
        else if (Variables.Instance.timespan.TotalMinutes > Minutes && Variables.Instance.timespan.TotalMinutes < Minutes * 2 && changeMat)
        {
            changeMat = false;
            StartCoroutine(ChangeOverTime(0, 1, 5));
        }
        else if (Variables.Instance.timespan.TotalMinutes > Minutes * 2 && changeNextMat)
        {
            changeNextMat = false;
            StartCoroutine(ChangeOverTime(1, 2, 5));
        }
    }

    private IEnumerator ChangeOverTime(int materialIndex, int material, float duration)
    {
        //float planetDisplacement_Initial = planet.Displacement;
        //float planetWaterLevel_Initial = planet.WaterLevel;
        Color planetWaterGradientShallow_Initial = planetWaterGradient.Shallow;
        Color planetWaterGradientDeep_Initial = planetWaterGradient.Deep;
        float planetWaterGradientSharpness_Initial = planetWaterGradient.Sharpness;
        float planetWaterGradientScale_Initial = planetWaterGradient.Scale;
        Color cloudsphereColor_Initial = cloudsphere.Color;
        Color cloudsphereAmbientColor_Initial = cloudsphere.AmbientColor;
        float cloudsphereRadius_Initial = cloudsphere.Radius;
        Color cloudsphereDepthRimColor_Initial = cloudsphereDepth.RimColor;
        Color atmosphereColor_Initial = atmosphere.Color;
        Color atmosphereHorizonColor_Initial = atmosphereDepth.HorizonColor;
        Color atmosphereInnerColor_Initial = atmosphereDepth.InnerColor;
        Color atmosphereOuterColor_Initial = atmosphereDepth.OuterColor;

        float time = 0f;
        while (time < duration)
        {
            //planet.Displacement = Mathf.Lerp(planetDisplacement_Initial, _Displacement[materialIndex], time / duration);
            //planet.WaterLevel = Mathf.Lerp(planetWaterLevel_Initial, _WaterLevel[materialIndex], time / duration);

            planetWaterGradient.Shallow = Color.Lerp(planetWaterGradientShallow_Initial, _Shallow[materialIndex], time / duration);
            planetWaterGradient.Deep = Color.Lerp(planetWaterGradientDeep_Initial, _Deep[materialIndex], time / duration);
            planetWaterGradient.Sharpness = Mathf.Lerp(planetWaterGradientSharpness_Initial, _Sharpness[materialIndex], time / duration);
            planetWaterGradient.Scale = Mathf.Lerp(planetWaterGradientScale_Initial, _Scale[materialIndex], time / duration);


            cloudsphere.Color = Color.Lerp(cloudsphereColor_Initial, _CColor[materialIndex], time / duration);
            cloudsphere.AmbientColor = Color.Lerp(cloudsphereAmbientColor_Initial, _AmbientColor[materialIndex], time / duration);
            cloudsphere.Radius = Mathf.Lerp(cloudsphereRadius_Initial, _Radius[materialIndex], time / duration);
            cloudsphereDepth.RimColor = Color.Lerp(cloudsphereDepthRimColor_Initial, _RimColor[materialIndex], time / duration);

            atmosphere.Color = Color.Lerp(atmosphereColor_Initial, _AColor[materialIndex], time / duration);
            atmosphereDepth.HorizonColor = Color.Lerp(atmosphereHorizonColor_Initial, _HorizonColor[materialIndex], time / duration);
            atmosphereDepth.InnerColor = Color.Lerp(atmosphereInnerColor_Initial, _InnerColor[materialIndex], time / duration);
            atmosphereDepth.OuterColor = Color.Lerp(atmosphereOuterColor_Initial, _OuterColor[materialIndex], time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(SwitchPlanet(transform.localScale, new Vector3(0, 0, 0), 0.3f));
        planet.Material = materials[material];
        planet.Displacement = _Displacement[materialIndex];
        planet.WaterLevel = _WaterLevel[materialIndex];
        yield return StartCoroutine(SwitchPlanet(transform.localScale, new Vector3(1, 1, 1), 0.3f));
    }

    public IEnumerator SwitchPlanet(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, Mathf.SmoothStep(0.0f, 1.0f, i));
            yield return null;
        }
    }
}
