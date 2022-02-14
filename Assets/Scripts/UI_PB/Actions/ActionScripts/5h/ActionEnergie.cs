using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEnergie : MonoBehaviour
{
    public void EnergySwitch()
    {
        Variables.Instance.h_energy -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
