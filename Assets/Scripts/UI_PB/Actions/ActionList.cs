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

    void Update()
    {

    }

    public void OpenAction()
    {
        transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        GameObject newAction = Instantiate(action[Variables.Instance.currentActionIndex - _menu.historyContent.Length], transform); //Index - Vorherige ScriptableObjects
        newAction.GetComponentInChildren<Canvas>().worldCamera = UICam;
        newAction.name = Variables.Instance.currentActionIndex.ToString();

        _menu.AppendHistory(Variables.Instance.currentActionIndex);
    }
}
