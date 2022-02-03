using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endcard : MonoBehaviour
{
    public float waterUseRate_itemCount;
    public float reproductionRate_itemCount;
    public float waterStorageRate_itemCount;

    void Start()
    {
        waterUseRate_itemCount = Variables.Instance.waterUseRate / 0.5f;
        reproductionRate_itemCount = Variables.Instance.reproductionRate / 0.3f;
        waterStorageRate_itemCount = Variables.Instance.waterStorageRate / 0.2f;
    }

    void Update()
    {
        
    }
}
