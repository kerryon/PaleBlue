using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{

    private bool toggleBool = false;
    public GameObject menuBtn;
    public GameObject menuScreen;
    public GameObject StatisticsOverlay;
    public GameObject GoalsOverlay;
    public Animator menuTrigger;
    public Animator menuBtnTrigger;
    public Animator transition;

    public Animator textboxTrigger;
    public GameObject Textbox, Levels;
    public GameObject prefab;
    public GameObject NameDisplay;
    public GameObject LifeNameDisplay;

    private bool statisticsToggle = false;
    private bool goalsToggle = false;

    public void Awake()
    {
        menuBtn.SetActive(false);
        menuScreen.SetActive(false);
        Textbox.SetActive(false);

        for (int i = 1; i < Levels.transform.childCount; i++)
        {
            Levels.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(ShowMenuButton());
        NewScene();
        NameDisplay.GetComponent<TMP_Text>().text = (string)ES3.Load("NAME");
        LifeNameDisplay.GetComponent<TMP_Text>().text = (string)ES3.Load("LIFE");
    }

    IEnumerator ShowMenuButton()
    {
        yield return new WaitForSeconds(15);
        menuBtn.SetActive(true);
    }

    public void NewScene()
    {
        if (Levels.transform.childCount >= 3)
        {
            for (int i = 2; i < Variables.Instance.historyCount; i++)
            {
                Destroy(Levels.transform.GetChild(i));
            }
        }

        Variables.Instance.historyCount++;

        for (int i = 0; i < Variables.Instance.historyCount; i++)
        {
            GameObject newHistoryObject = Instantiate(prefab, Levels.transform);
            newHistoryObject.name = "History" + i;
            newHistoryObject.SetActive(true);
        }

        menuScreen.SetActive(false);
    }

    public void ToggleMenu()
    {
        StartCoroutine(MenuToggle());
    }

    IEnumerator MenuToggle()
    {
        menuBtnTrigger.SetTrigger("MenuBtnClicked");

        if (toggleBool == true)
        {
            menuTrigger.SetTrigger("MenuTrigger");
            yield return new WaitForSeconds(1);
        }
        toggleBool = !toggleBool;
        menuScreen.SetActive(toggleBool);
    }

    public void BackToMenu()
    {
        StartCoroutine(MenuTransition());
    }

    IEnumerator MenuTransition()
    {
        transition.SetTrigger("LevelLoadStart");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void ToggleTextbox()
    {
        StartCoroutine(TextboxToggle());
    }

    IEnumerator TextboxToggle()
    {
        textboxTrigger.SetTrigger("MenuInfoToggle");

        yield return new WaitForSeconds(1);

        Textbox.SetActive(false);
    }

    public void ToggleStatistics()
    {
        statisticsToggle = !statisticsToggle;
        StatisticsOverlay.SetActive(statisticsToggle);
    }

    public void ToggleGoals()
    {
        goalsToggle = !goalsToggle;
        GoalsOverlay.SetActive(goalsToggle);
    }
}
