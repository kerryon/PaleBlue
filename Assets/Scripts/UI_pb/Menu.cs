﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

    public GameObject infoWindow;

    public Animator textboxTrigger;
    public GameObject Textbox, Levels;
    public GameObject prefab;
    public GameObject NameDisplay;
    public GameObject LifeNameDisplay;

    [Header("Scriptable Objects")]
    public ScriptableObjectContent[] historyContent;
    public PieElement[] actionContent;

    private bool statisticsToggle = false;
    private bool goalsToggle = false;
    private bool textboxToggle = false;

    private bool historyLoaded = false;

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
        if (Variables.Instance.historyCount == 1)
        {
            CreateHistory();
        } else
        {
            AppendHistory(0);
        }
        NameDisplay.GetComponent<TMP_Text>().text = (string)ES3.Load("NAME");
        LifeNameDisplay.GetComponent<TMP_Text>().text = (string)ES3.Load("LIFE");
    }

    IEnumerator ShowMenuButton()
    {
        yield return new WaitForSeconds(15);
        menuBtn.SetActive(true);
    }

    public void CreateHistory()
    {
        var sheet = new ES3Spreadsheet();

        for (int row = 0; row < Variables.Instance.historyCount; row++)
        {
            sheet.SetCell(0, row, row);
            sheet.SetCell(1, row, 0);
        }

        sheet.Save("history.csv");

        GameObject newHistoryObject = Instantiate(prefab, Levels.transform);
        newHistoryObject.name = "History" + 0;
        newHistoryObject.SetActive(true);

        menuScreen.SetActive(false);
    }

    public void AppendHistory(int ScriptableObjectValue)
    {
        var sheet = new ES3Spreadsheet();

        if (Levels.transform.childCount == 2)
        {
            sheet.Load("history.csv");

            for (int i = 0; i < Variables.Instance.historyCount; i++)
            {
                GameObject newHistoryObject = Instantiate(prefab, Levels.transform);
                newHistoryObject.name = "History" + i;
                newHistoryObject.SetActive(true);
            }
        }
        else
        {
            sheet.SetCell(0, 0, Variables.Instance.historyCount);
            sheet.SetCell(1, 0, ScriptableObjectValue);

            GameObject newHistoryObject = Instantiate(prefab, Levels.transform);
            newHistoryObject.name = "History" + Variables.Instance.historyCount;
            newHistoryObject.SetActive(true);

            sheet.Save("history.csv", true);
            Variables.Instance.historyCount++;
        }

        historyLoaded = false;
        menuScreen.SetActive(false);
    }

    public void LoadHistory()
    {
        if (!historyLoaded)
        {
            var sheet = new ES3Spreadsheet();
            sheet.Load("history.csv");

            for (int i = 0; i < Variables.Instance.historyCount; i++)
            {
                Image titleImage = Levels.transform.GetChild(i + 2).GetComponent<Image>();
                TMP_Text title = Levels.transform.GetChild(i + 2).GetChild(0).GetComponent<TMP_Text>();
                titleImage.sprite = historyContent[sheet.GetCell<int>(1, i)].titleImage;
                title.text = historyContent[sheet.GetCell<int>(1, i)].topic;
            }
            historyLoaded = true;
        }
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
        if (textboxToggle)
        {
            textboxTrigger.SetTrigger("MenuInfoToggle");
            yield return new WaitForSeconds(1);
        }
        textboxToggle = !textboxToggle;
        Textbox.SetActive(textboxToggle);
    }

    public void ToggleStatistics()
    {
        statisticsToggle = !statisticsToggle;
        StartCoroutine(ToggleStatisticsCoroutine());
    }

    IEnumerator ToggleStatisticsCoroutine()
    {
        if (!statisticsToggle)
        {
            StatisticsOverlay.GetComponent<Animator>().SetTrigger("StatisticsMenuClose");
            yield return new WaitForSeconds(0.5f);
        }
        StatisticsOverlay.SetActive(statisticsToggle);
    }

    public void ToggleGoals()
    {
        goalsToggle = !goalsToggle;
        StartCoroutine(ToggleGoalsCoroutine());
    }

    IEnumerator ToggleGoalsCoroutine()
    {
        if (!goalsToggle)
        {
            GoalsOverlay.GetComponent<Animator>().SetTrigger("GoalsMenuClose");
            yield return new WaitForSeconds(0.5f);
        }
        GoalsOverlay.SetActive(goalsToggle);
    }

    public void OpenInfo()
    {
        infoWindow.SetActive(true);
    }

    public void CloseInfo()
    {
        StartCoroutine(CloseInfoCoroutine());
    }

    IEnumerator CloseInfoCoroutine()
    {
        infoWindow.GetComponent<Animator>().SetTrigger("MenuScreenInfoTrigger");
        yield return new WaitForSeconds(1);
        infoWindow.SetActive(false);
    }
}
