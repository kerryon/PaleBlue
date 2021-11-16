using System.Collections;
using TMPro;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera UICamera;
    public TMP_Text text;
    private Menu _menu;
    private GameObject buttons;

    LevelLoader levelLoader;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        buttons = gameObject.transform.GetChild(0).gameObject;

        text.gameObject.SetActive(false);

        levelLoader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelLoader.attachCam(UICamera);
    }

    void Update()
    {
        if (!buttons.activeInHierarchy)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended) //oder .Moved???
                {
                    buttons.SetActive(true);
                }
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            buttons.SetActive(true);
        }
#endif
    }

    public void StatisticsToggle()
    {
        _menu.ToggleStatistics();
    }

    public void GoalsToggle()
    {
        _menu.ToggleGoals();
    }
}
