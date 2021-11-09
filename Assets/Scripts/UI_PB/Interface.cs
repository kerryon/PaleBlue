using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    private Menu _menu;
    private GameObject buttons;
    private bool showHide = true;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        buttons = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (!buttons.activeInHierarchy)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) //oder .Moved???
                {
                    buttons.SetActive(true);
                }
            }
        }
    }

    public void StatisticsToggle()
    {
        buttons.SetActive(!showHide);
        _menu.ToggleStatistics();
    }

    public void GoalsToggle()
    {
        buttons.SetActive(!showHide);
        _menu.ToggleGoals();
    }
}
