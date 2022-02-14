using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPipelines : MonoBehaviour
{
    public void CreatePipelines()
    {
        Variables.Instance.h_waterStructure -= 20000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
