using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMeeresschutz : MonoBehaviour
{
    public void CreateOceanSafeZones()
    {
        Variables.Instance.h_overfishing -= 30000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
