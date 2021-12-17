using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public GameObject prefab;

    public GameObject dot1;
    public GameObject dot2;
    public GameObject dot3;

    private GameObject g1;
    private GameObject g2;
    private GameObject g3;

    public TMP_Text planetTitle;
    public TMP_Text creatureTitle;
    public TMP_Text creatureCopy;

    [Header("Scriptable Objects")]
    public ScriptableObjectCreatureDescription[] CardDescription;

    void Start()
    {
        planetTitle.text = (string)ES3.Load("NAME");
        creatureTitle.text = (string)ES3.Load("LIFE");
        creatureCopy.text = CardDescription[0].description;

        for (float i = 0; i <= Variables.Instance.waterUseRate; i += 0.1f)
        {
            g1 = Instantiate(prefab, transform.position, transform.rotation);
            g1.transform.SetParent(dot1.transform);
            g1.transform.localScale = new Vector3(1, 1, 1);
            g1.SetActive(true);

        }

        for (float i = 0; i <= Variables.Instance.reproductionRate; i += 0.1f)
        {
            g2 = Instantiate(prefab, transform.position, transform.rotation);
            g2.transform.SetParent(dot2.transform);
            g2.transform.localScale = new Vector3(1, 1, 1);
            g2.SetActive(true);
        }

        for (float i = 0; i <= Variables.Instance.waterStorageRate; i += 0.1f)
        {
            g3 = Instantiate(prefab, transform.position, transform.rotation);
            g3.transform.SetParent(dot3.transform);
            g3.transform.localScale = new Vector3(1, 1, 1);
            g3.SetActive(true);
        }
    }
}
