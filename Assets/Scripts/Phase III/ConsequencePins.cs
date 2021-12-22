using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsequencePins : MonoBehaviour
{
    public GameObject prefab;
    public float PlanetRadius;
    public GameObject PlanetOrigin;

    public Sprite[] sprites;

    private float[] waterSystem;
    private readonly float wv = 50000f;

    void Start()
    {
        InvokeRepeating(nameof(CheckConsequences), 0f, 10f);
    }

    private void CheckConsequences()
    {
        waterSystem = new float[] { Variables.Instance.w_distribution, Variables.Instance.w_current, Variables.Instance.w_contamination, Variables.Instance.w_temperature, Variables.Instance.w_weatherExtremes, Variables.Instance.w_carbonDioxide, Variables.Instance.w_fishCount, Variables.Instance.w_groundwater, Variables.Instance.w_trees, Variables.Instance.w_ice };

        for (int i = 0; i < waterSystem.Length; i++)
        {
            if (waterSystem[i] < wv)
            {
                if (!transform.Find(i.ToString()))
                {
                    CreatePinPrefab(i);
                }
            }
            else
            {
                if (transform.Find(i.ToString()))
                {
                    Destroy(transform.Find(i.ToString()).gameObject);
                }
            }
        } 
    }

    public void CreatePinPrefab(int pin)
    {
        Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[pin];
        GameObject newObject = Instantiate(prefab, onPlanet, Quaternion.identity);
        newObject.SetActive(true);
        newObject.name = pin.ToString();
        newObject.transform.LookAt(PlanetOrigin.transform.position);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
        newObject.transform.parent = gameObject.transform;

        var sheet = new ES3Spreadsheet();
        sheet.Load("history.csv");
        for (int i = 0; i < Variables.Instance.historyCount; i++)
        {
            if (pin == sheet.GetCell<int>(1, i))
            {
                newObject.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 96, 96, 255);
                return;
            }
        }
    }
}
