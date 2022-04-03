using System.Collections;
using TMPro;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera UICamera;
    public GameObject actionMenu;
    private Menu _menu;
    private GameObject buttons;
    private bool actionToggle = false;

    LevelLoader levelLoader;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        buttons = gameObject.transform.GetChild(0).gameObject;

        levelLoader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelLoader.AttachCam(UICamera);

        Invoke(nameof(EnableButtons), 5f);
    }

    private void EnableButtons()
    {
        buttons.SetActive(true);
    }

    public void StatisticsToggle()
    {
        _menu.ToggleStatistics();
    }

    public void GoalsToggle()
    {
        _menu.ToggleGoals();
    }

    public void ActionToggle()
    {
        StartCoroutine(ActionToggleCoroutine());
    }

    IEnumerator ActionToggleCoroutine()
    {
        Transform actionPieWrapper = actionMenu.transform.GetChild(1);
        if (actionPieWrapper.childCount > 1)
        {
            actionPieWrapper.GetChild(0).gameObject.SetActive(true);
            for (int i = 1; i < actionPieWrapper.childCount; i++)
            {
                Destroy(actionPieWrapper.GetChild(i).gameObject);
            }
        }
        else
        {
            actionToggle = !actionToggle;

            if (!actionToggle)
            {
                actionMenu.GetComponent<Animator>().SetTrigger("ActionMenuClose");
                yield return new WaitForSeconds(0.5f);
            }
            actionMenu.SetActive(actionToggle);
        }
    }
}
