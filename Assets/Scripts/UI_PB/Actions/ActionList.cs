using System;
using System.Collections;
using Lean.Gui;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    public Camera UICam;
    public GameObject[] action;

    Menu _menu;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
    }

    public void OpenAction()
    {
        GameObject buttons = transform.parent.gameObject.transform.GetChild(0).gameObject;
        buttons.SetActive(false);
        int num = Variables.Instance.currentActionIndex - _menu.historyContent.Length;

        if (!string.IsNullOrEmpty(_menu.actionContent[num].pathName))
        {
            int cost = Convert.ToInt32(_menu.actionContent[num].pathName);
            if ((Variables.Instance.actionHours - cost) >= 0)
            {
                Variables.Instance.actionHours -= cost;
            }
            else
            {
                buttons.SetActive(true);
                StartCoroutine(ToggleNotification());
                return;
            }
        }

        GameObject newAction = Instantiate(action[num], transform); //Index - Vorherige ScriptableObjects
        newAction.GetComponentInChildren<Canvas>().worldCamera = UICam;
        newAction.name = Variables.Instance.currentActionIndex.ToString();
 
        _menu.AppendHistory(Variables.Instance.currentActionIndex);
    }

    IEnumerator ToggleNotification()
    {
        GameObject notification = transform.GetChild(0).gameObject;
        notification.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<LeanPulse>().Pulse();
        yield return new WaitForSeconds(7);
        notification.SetActive(false);
    }
}
