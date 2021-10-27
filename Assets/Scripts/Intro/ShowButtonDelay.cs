using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowButtonDelay : MonoBehaviour
{
    public GameObject button;

    void Start()
    {
        button.SetActive(false);

        Invoke("DisplayButton", 5f);
    }

    void DisplayButton()
    {
        button.SetActive(true);
    }
}
