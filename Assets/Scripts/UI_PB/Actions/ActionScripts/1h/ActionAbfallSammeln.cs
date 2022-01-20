using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionAbfallSammeln : MonoBehaviour
{
    public GameObject text;
    public GameObject itemInfo;

    void Start()
    {
        transform.GetChild(1).GetComponent<SpaceGraphicsToolkit.SgtTerrainPrefabSpawner>().SharedMaterial = GameObject.FindGameObjectWithTag("Atmosphere").GetComponent<SpaceGraphicsToolkit.SgtSharedMaterial>();

        itemInfo.SetActive(false);
    }

    void Update()
    {
        
    }

    public void PickTrash()
    {
        if (text.activeSelf)
        {
            text.SetActive(false);
        }

        itemInfo.SetActive(true);
        Variables.Instance.h_waste -= 5000f;
    }

    public void StopAction()
    {
        transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject, 1.2f);
    } 
}
