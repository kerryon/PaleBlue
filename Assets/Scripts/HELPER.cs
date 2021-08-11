using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HELPER : MonoBehaviour
{
    public GameObject txt;

    void Start()
    {
        
    }

    public void HideTxt()
    {
        txt.SetActive(false);
    }

    public void HideAll()
    {
        gameObject.SetActive(false);
    }
}
