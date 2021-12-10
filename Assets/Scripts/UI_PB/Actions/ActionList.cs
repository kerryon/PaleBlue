using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public Camera UICam;
    public GameObject[] action;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenAction()
    {
        GameObject newAction = Instantiate(action[0], transform); //[Variables.Instance.currentActionIndex]
        newAction.GetComponentInChildren<Canvas>().worldCamera = UICam;
    }
}
