using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        for (float i = 0; i <= Variables.Instance.waterUseRate; i += 0.05f)
        {
            g1 = Instantiate(prefab, transform.position, transform.rotation);
            g1.transform.SetParent(dot1.transform);
            g1.SetActive(true);

        }

        for (float i = 0; i <= Variables.Instance.reproductionRate; i += 0.03f)
        {
            g2 = Instantiate(prefab, transform.position, transform.rotation);
            g2.transform.SetParent(dot2.transform);
            g2.SetActive(true);
        }

        for (float i = 0; i <= Variables.Instance.waterStorageRate; i += 0.02f)
        {
            g3 = Instantiate(prefab, transform.position, transform.rotation);
            g3.transform.SetParent(dot3.transform);
            g3.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
