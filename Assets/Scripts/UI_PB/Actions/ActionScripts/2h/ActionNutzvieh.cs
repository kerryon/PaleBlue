using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNutzvieh : MonoBehaviour
{
    public void CancelMeatConsumption()
    {
        Variables.Instance.h_agriculture -= 10000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
