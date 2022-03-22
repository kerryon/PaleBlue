using UnityEngine;
using SpaceGraphicsToolkit.Belt;

public class AsteroideBeltVanish : MonoBehaviour
{
    private SgtBeltSimple asteroides;
    private readonly int initialAsteroidCount = 7000;

    void Start()
    {
        asteroides = GetComponent<SgtBeltSimple>();
    }

    void Update()
    {
        if (asteroides.AsteroidCount > 0 && Variables.Instance.timespan.TotalSeconds < initialAsteroidCount)
        {
            asteroides.AsteroidCount = (int)(initialAsteroidCount - Variables.Instance.timespan.TotalSeconds);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
