using UnityEngine;
using SpaceGraphicsToolkit;

public class AsteroideBeltVanish : MonoBehaviour
{
    private SgtBeltSimple asteroides;

    void Start()
    {
        asteroides = GetComponent<SgtBeltSimple>();
    }

    void Update()
    {
        if (asteroides.AsteroidCount == 0)
        {
            Destroy(gameObject);
        } else
        {
            asteroides.AsteroidCount = (int)(7000 - (Variables.Instance.timespan.TotalMinutes * 1000));
        }
    }
}
