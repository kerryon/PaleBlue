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

    public GameObject[] additionalAssets;
    public Material[] materials;

    private bool changeMat = true;
    [Header("New Planet Variables", order = 1)]
    [Space(10)]
    [Header("SgtPlanet", order = 2)]
    public float _Displacement;
    public float _WaterLevel;
    [Header("SgtPlanetWaterGradient")]
    public Color _Shallow;
    public Color _Deep;
    public float _Sharpness;
    public float _Scale;
    [Header("SgtCloudsphere")]
    public Color _CColor;
    public Color _AmbientColor;
    public float _Radius;
    [Header("SgtCloudsphereDepthTex")]
    public Color _RimColor;
    [Header("SgtAtmopshere")]
    public Color _AColor;
    [Header("SgtAtmosphereDepthTex")]
    public Color _HorizonColor;
    public Color _InnerColor;
    public Color _OuterColor;

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
            planet.WaterLevel = Mathf.Lerp(0.0f, 0.45f, (float)(Variables.Instance.timespan.TotalSeconds / (Minutes * 60)));
        }
        else if (Variables.Instance.timespan.TotalMinutes > Minutes && changeMat)
        {
            changeMat = false;
            StartCoroutine(ChangeOverTime(10, 1));
        }
    }

    private IEnumerator ChangeOverTime(float duration, int material)
    {
        float planetDisplacement_Initial = planet.Displacement;
        float planetWaterLevel_Initial = planet.WaterLevel;
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
            planet.Displacement = Mathf.Lerp(planetDisplacement_Initial, _Displacement, time / duration); //notwendig?
            planet.WaterLevel = Mathf.Lerp(planetWaterLevel_Initial, _WaterLevel, time / duration); //notwendig?

            planetWaterGradient.Shallow = Color.Lerp(planetWaterGradientShallow_Initial, _Shallow, time / duration);
            planetWaterGradient.Deep = Color.Lerp(planetWaterGradientDeep_Initial, _Deep, time / duration);
            planetWaterGradient.Sharpness = Mathf.Lerp(planetWaterGradientSharpness_Initial, _Sharpness, time / duration);
            planetWaterGradient.Scale = Mathf.Lerp(planetWaterGradientScale_Initial, _Scale, time / duration);


            cloudsphere.Color = Color.Lerp(cloudsphereColor_Initial, _CColor, time / duration);
            cloudsphere.AmbientColor = Color.Lerp(cloudsphereAmbientColor_Initial, _AmbientColor, time / duration);
            cloudsphere.Radius = Mathf.Lerp(cloudsphereRadius_Initial, _Radius, time / duration);
            cloudsphereDepth.RimColor = Color.Lerp(cloudsphereDepthRimColor_Initial, _RimColor, time / duration);

            atmosphere.Color = Color.Lerp(atmosphereColor_Initial, _AColor, time / duration);
            atmosphereDepth.HorizonColor = Color.Lerp(atmosphereHorizonColor_Initial, _HorizonColor, time / duration);
            atmosphereDepth.InnerColor = Color.Lerp(atmosphereInnerColor_Initial, _InnerColor, time / duration);
            atmosphereDepth.OuterColor = Color.Lerp(atmosphereOuterColor_Initial, _OuterColor, time / duration);

            time += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(SwitchPlanet(transform.localScale, new Vector3(0, 0, 0), 0.3f));
        planet.Material = materials[material];
        planet.Displacement = _Displacement;
        planet.WaterLevel = _WaterLevel;
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

    //private Color HexToColor(string hex)
    //{
    //    ColorUtility.TryParseHtmlString(hex, out Color color);
    //    return color;
    //}
}
