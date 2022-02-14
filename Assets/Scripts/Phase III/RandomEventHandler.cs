using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventHandler : MonoBehaviour
{
    public GameObject prefab;
    public float PlanetRadius;

    public Sprite[] sprites;

    public void CreatePinPrefab(int pin)
    {
        Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[pin];
        GameObject newObject = Instantiate(prefab, onPlanet, Quaternion.identity, gameObject.transform);
        newObject.SetActive(true);
        newObject.name = pin.ToString();
        newObject.transform.LookAt(Vector3.zero);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
        newObject.transform.localScale = new Vector3(7,7,7);
    }
}
