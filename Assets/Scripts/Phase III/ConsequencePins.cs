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
        waterSystem = new float[] { Variables.Instance.w_groundwater, Variables.Instance.w_trees, Variables.Instance.w_temperature, Variables.Instance.w_current, Variables.Instance.w_carbonDioxide, Variables.Instance.w_weatherExtremes, Variables.Instance.w_ice, Variables.Instance.w_fishCount, Variables.Instance.w_contamination, Variables.Instance.w_distribution, Mathf.Lerp(100000f, 49999f, Variables.Instance.rain/300f) };

        for (int i = 0; i < waterSystem.Length; i++)
        {
            if (waterSystem[i] < wv)
            {
                switch(i)
                {
                    case 0:
                        if (!GameObject.FindGameObjectWithTag("Pin0"))
                        {
                            CreatePinPrefab(0);
                        }
                        break;
                    case 1:
                        if (!GameObject.FindGameObjectWithTag("Pin1"))
                        {
                            CreatePinPrefab(1);
                        }
                        if (!GameObject.FindGameObjectWithTag("Pin11"))
                        {
                            CreatePinPrefab(11);
                        }
                        break;
                    case 2:
                        if (!GameObject.FindGameObjectWithTag("Pin2"))
                        {
                            CreatePinPrefab(2);
                        }
                        break;
                    case 3:
                        if (!GameObject.FindGameObjectWithTag("Pin2"))
                        {
                            CreatePinPrefab(2);
                        }
                        break;
                    case 4:
                        if (!GameObject.FindGameObjectWithTag("Pin3"))
                        {
                            CreatePinPrefab(3);
                        }
                        break;
                    case 5:
                        if (!GameObject.FindGameObjectWithTag("Pin4"))
                        {
                            CreatePinPrefab(4);
                        }
                        if (!GameObject.FindGameObjectWithTag("Pin11"))
                        {
                            CreatePinPrefab(11);
                        }
                        break;
                    case 6:
                        if (!GameObject.FindGameObjectWithTag("Pin5"))
                        {
                            CreatePinPrefab(5);
                        }
                        if (!GameObject.FindGameObjectWithTag("Pin10"))
                        {
                            CreatePinPrefab(10);
                        }
                        break;
                    case 7:
                        if (!GameObject.FindGameObjectWithTag("Pin6"))
                        {
                            CreatePinPrefab(6);
                        }
                        break;
                    case 8:
                        if (!GameObject.FindGameObjectWithTag("Pin7"))
                        {
                            CreatePinPrefab(7);
                        }
                        break;
                    case 9:
                        if (!GameObject.FindGameObjectWithTag("Pin8"))
                        {
                            CreatePinPrefab(8);
                        }
                        break;
                    case 10:
                        if (!GameObject.FindGameObjectWithTag("Pin9"))
                        {
                            CreatePinPrefab(9);
                        }
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        if (GameObject.FindGameObjectWithTag("Pin0"))
                        {
                            DestroyPinPrefab("Pin0");
                        }
                        break;
                    case 1:
                        if (GameObject.FindGameObjectWithTag("Pin1"))
                        {
                            DestroyPinPrefab("Pin1");
                        }
                        if (GameObject.FindGameObjectWithTag("Pin11"))
                        {
                            DestroyPinPrefab("Pin11");
                        }
                        break;
                    case 2:
                        if (GameObject.FindGameObjectWithTag("Pin2"))
                        {
                            DestroyPinPrefab("Pin2");
                        }
                        break;
                    case 3:
                        if (GameObject.FindGameObjectWithTag("Pin2"))
                        {
                            DestroyPinPrefab("Pin2");
                        }
                        break;
                    case 4:
                        if (GameObject.FindGameObjectWithTag("Pin3"))
                        {
                            DestroyPinPrefab("Pin3");
                        }
                        break;
                    case 5:
                        if (GameObject.FindGameObjectWithTag("Pin4"))
                        {
                            DestroyPinPrefab("Pin4");
                        }
                        if (GameObject.FindGameObjectWithTag("Pin11"))
                        {
                            DestroyPinPrefab("Pin11");
                        }
                        break;
                    case 6:
                        if (GameObject.FindGameObjectWithTag("Pin5"))
                        {
                            DestroyPinPrefab("Pin5");
                        }
                        if (GameObject.FindGameObjectWithTag("Pin10"))
                        {
                            DestroyPinPrefab("Pin10");
                        }
                        break;
                    case 7:
                        if (GameObject.FindGameObjectWithTag("Pin6"))
                        {
                            DestroyPinPrefab("Pin6");
                        }
                        break;
                    case 8:
                        if (GameObject.FindGameObjectWithTag("Pin7"))
                        {
                            DestroyPinPrefab("Pin7");
                        }
                        break;
                    case 9:
                        if (GameObject.FindGameObjectWithTag("Pin8"))
                        {
                            DestroyPinPrefab("Pin8");
                        }
                        break;
                    case 10:
                        if (GameObject.FindGameObjectWithTag("Pin9"))
                        {
                            DestroyPinPrefab("Pin9");
                        }
                        break;
                }
            }
        } 
    }

    public void DestroyPinPrefab(string pin)
    {
        Destroy(GameObject.FindGameObjectWithTag(pin));
    }

    public void CreatePinPrefab(int pin)
    {
        Vector3 onPlanet = Random.onUnitSphere * PlanetRadius;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = sprites[pin];
        GameObject newObject = Instantiate(prefab, onPlanet, Quaternion.identity);
        newObject.SetActive(true);
        newObject.name = pin.ToString();
        newObject.tag = "Pin" + pin;
        newObject.transform.LookAt(PlanetOrigin.transform.position);
        newObject.transform.rotation = newObject.transform.rotation * Quaternion.Euler(-90, 0, 0);
        newObject.transform.parent = gameObject.transform;

        var sheet = new ES3Spreadsheet();
        sheet.Load("history.csv");
        for (int i = 0; i < Variables.Instance.historyCount; i++)
        {
            if ((pin + 11) == sheet.GetCell<int>(1, i))
            {
                newObject.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 96, 96, 255);
                return;
            }
        }
    }
}
