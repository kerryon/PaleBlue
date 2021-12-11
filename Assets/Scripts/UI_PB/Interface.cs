using System.Collections;
using TMPro;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public Camera UICamera;
    public TMP_Text text;
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
        levelLoader.attachCam(UICamera);

        text.gameObject.SetActive(false);

        Invoke(nameof(EnableButtons), 5f);
    }

    void Update()
    {
        //if (!buttons.activeInHierarchy)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        Touch touch = Input.GetTouch(0);

        //        if (touch.phase == TouchPhase.Ended) //oder .Moved???
        //        {
        //            buttons.SetActive(true);
        //        }
        //    }
        //}

//#if UNITY_EDITOR
//        if (Input.GetMouseButtonDown(0))
//        {
//            buttons.SetActive(true);
//        }
//#endif
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
        actionToggle = !actionToggle;
        StartCoroutine(ActionToggleCoroutine());
    }

    IEnumerator ActionToggleCoroutine()
    {
        if (!actionToggle)
        {
            actionMenu.GetComponent<Animator>().SetTrigger("ActionMenuClose");
            yield return new WaitForSeconds(0.5f);
        }
        actionMenu.SetActive(actionToggle);
    }
}
