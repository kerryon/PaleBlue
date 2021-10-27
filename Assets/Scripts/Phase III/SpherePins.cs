using UnityEngine;

public class SpherePins : MonoBehaviour
{
    private int PinCount = 0;
    public GameObject prefab;
    public float PlanetRadius;
    public GameObject PlanetOrigin;

    private int interval = 1;
 
    public Sprite[] sprites;

    void Update()
    {
        if (Variables.Instance.timespan.TotalMinutes > interval)
        {
            interval++;
            CreatePinPrefab(PinCount);
            PinCount++;
        }
    }

    public void CreatePinPrefab(int pinCount)
    {
        Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[pinCount];
        GameObject newObject = Instantiate(prefab, onPlanet, Quaternion.identity);
        newObject.name = pinCount.ToString();
        newObject.transform.LookAt(PlanetOrigin.transform.position);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
        newObject.transform.parent = gameObject.transform;
    }
}