using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGesetze : MonoBehaviour
{
    public void SetLaw()
    {
        Variables.Instance.h_industry -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
