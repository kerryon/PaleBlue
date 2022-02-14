using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionForschen : MonoBehaviour
{
    public void Research()
    {
        int randomField = Random.Range(0, 9);

        switch(randomField)
        {
            case 0:
                Variables.Instance.h_conflict -= 10000f;
                break;
            case 1:
                Variables.Instance.h_luxury -= 10000f;
                break;
            case 2:
                Variables.Instance.h_industry -= 10000f;
                break;
            case 3:
                Variables.Instance.h_agriculture -= 10000f;
                break;
            case 4:
                Variables.Instance.h_waste -= 10000f;
                break;
            case 5:
                Variables.Instance.h_urbanisation -= 10000f;
                break;
            case 6:
                Variables.Instance.h_energy -= 10000f;
                break;
            case 7:
                Variables.Instance.h_overfishing -= 10000f;
                break;
            case 8:
                Variables.Instance.h_wasteWater -= 10000f;
                break;
            case 9:
                Variables.Instance.h_waterStructure -= 10000f;
                break;
        }
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
