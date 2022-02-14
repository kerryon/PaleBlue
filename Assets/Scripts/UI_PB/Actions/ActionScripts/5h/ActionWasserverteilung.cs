using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWasserverteilung : MonoBehaviour
{
    public void DistributeWater()
    {
        Variables.Instance.h_waterStructure -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
