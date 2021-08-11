using UnityEngine;

public class SpherePins : MonoBehaviour
{
    public float number;
    public GameObject prefab;
    public float PlanetRadius;
    public GameObject PlanetOrigin;

    public Sprite[] sprites;

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;

            prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[i];
            GameObject newGO = Instantiate(prefab, onPlanet, Quaternion.identity);
            newGO.transform.LookAt(PlanetOrigin.transform.position);
            newGO.transform.rotation = newGO.transform.rotation * Quaternion.Euler(-90, 0, 0);
            newGO.transform.parent = gameObject.transform;

        }
    }
}
