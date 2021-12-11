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
        transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        GameObject newAction = Instantiate(action[Variables.Instance.currentActionIndex - 10], transform); //Index - Vorherige ScriptableObjects
        newAction.GetComponentInChildren<Canvas>().worldCamera = UICam;
        newAction.name = Variables.Instance.currentActionIndex.ToString();
    }
}
