using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMeeresschutz : MonoBehaviour
{
    public void CreateOceanSafeZones()
    {
        Variables.Instance.h_overfishing -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
