using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionKampagne : MonoBehaviour
{
    public void StartCampaign()
    {
        Variables.Instance.h_luxury -= 30000f;
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
