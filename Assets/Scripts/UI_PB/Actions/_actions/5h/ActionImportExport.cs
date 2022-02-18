using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionImportExport : MonoBehaviour
{
    public void WorldTrade()
    {
        Variables.Instance.h_agriculture -= 30000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
