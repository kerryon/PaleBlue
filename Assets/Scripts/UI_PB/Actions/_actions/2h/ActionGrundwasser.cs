using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGrundwasser : MonoBehaviour
{
    public void CreateGreenSpaces()
    {
        Variables.Instance.h_urbanisation -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
