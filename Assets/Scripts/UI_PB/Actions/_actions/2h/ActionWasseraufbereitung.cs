using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWasseraufbereitung : MonoBehaviour
{
    public void WaterTreatment()
    {
        Variables.Instance.h_wasteWater -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
