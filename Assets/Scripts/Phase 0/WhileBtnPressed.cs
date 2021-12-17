using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Common;
using SpaceGraphicsToolkit;

public class WhileBtnPressed : MonoBehaviour, IPointerUpHandler
{
    public GameObject planet;
    private SgtPlanet planetDisplacement;
    bool isAdding = false;
    bool isRemoving = false;
    private float initialDisplacement;

    void Start()
    {
        planetDisplacement = planet.GetComponent<SgtPlanet>();
        initialDisplacement = planetDisplacement.Displacement;

    }

    void Update()
    {
        if (isAdding)
        {
            planet.GetComponent<LeanManualRescale>().AddScaleA(0.1f);
            planetDisplacement.Displacement = Map(planet.transform.localScale.x, 10, 150, initialDisplacement, 0.22f);
            return;
        }
        if (isRemoving)
        {
            planet.GetComponent<LeanManualRescale>().AddScaleA(-0.1f);
            planetDisplacement.Displacement = Map(planet.transform.localScale.x, 10, 150, initialDisplacement, 0.22f);
            return;
        }
    }

    public static float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }

    public void AddMass()
    {
        isAdding = true;
    }

    public void RemoveMass()
    {
        isRemoving = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isAdding = false;
        isRemoving = false;
    }
}