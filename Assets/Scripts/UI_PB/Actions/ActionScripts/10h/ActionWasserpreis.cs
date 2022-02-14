using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWasserpreis : MonoBehaviour
{
    public void RegulateWaterPrice()
    {
        Variables.Instance.h_conflict -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
