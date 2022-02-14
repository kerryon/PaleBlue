using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionErfindungen : MonoBehaviour
{
    public void Invent(int area)
    {
        switch (area)
        {
            case 0:
                Variables.Instance.h_conflict -= 20000f;
                break;
            case 1:
                Variables.Instance.h_luxury -= 20000f;
                break;
            case 2:
                Variables.Instance.h_industry -= 20000f;
                break;
            case 3:
                Variables.Instance.h_agriculture -= 20000f;
                break;
            case 4:
                Variables.Instance.h_waste -= 20000f;
                break;
            case 5:
                Variables.Instance.h_urbanisation -= 20000f;
                break;
            case 6:
                Variables.Instance.h_energy -= 20000f;
                break;
            case 7:
                Variables.Instance.h_overfishing -= 20000f;
                break;
            case 8:
                Variables.Instance.h_wasteWater -= 20000f;
                break;
            case 9:
                Variables.Instance.h_waterStructure -= 20000f;
                break;
        }
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
