using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWerturteile : MonoBehaviour
{

    public void Choose()
    {

    }

    public void ExitAction()
    {
        // DO SOMETHING HERE
        transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject, 1.2f);
    }
}
