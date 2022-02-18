using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWasserverteilung : MonoBehaviour
{
    public void DistributeWater()
    {
        Variables.Instance.h_waterStructure -= 30000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
