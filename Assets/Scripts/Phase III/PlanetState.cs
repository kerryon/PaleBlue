using System.Collections;
using System.Collections.Generic;
using SpaceGraphicsToolkit;
using UnityEngine;

public class PlanetState : MonoBehaviour
{
    public Material[] mat;
    private SgtPlanet planet;

    void Start()
    {
        planet = GetComponent<SgtPlanet>();
    }

    void Update()
    {
        planet.WaterLevel = Mathf.Lerp(0.5f, 0.15f, Variables.Instance.waterSealevel);
    }
}
