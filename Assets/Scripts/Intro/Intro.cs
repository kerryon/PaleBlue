using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject thirdScreen;
    public GameObject fourthScreen;
    public GameObject meteorSpawner;
    public TMP_InputField planetName;
    public TMP_Text loadingAnimTxt;
    public GameObject loadingAnim;
    public Slider skipLoadingAnim;

    public int collisionCount = 0;

    private int EverythingDeleted = 0;

    private GameObject[] asteroids;

    public void Awake()
    {
        secondScreen.SetActive(false);
        thirdScreen.SetActive(false);
        fourthScreen.SetActive(false);
    }

    public void ScreenTwo()
    {
        firstScreen.SetActive(false);
        secondScreen.SetActive(true);
    }

    public void ScreenThree()
    {
        secondScreen.SetActive(false);
        thirdScreen.SetActive(true);
    }

    public void ScreenFour()
    {
        thirdScreen.SetActive(false);
        fourthScreen.SetActive(true);
        meteorSpawner.SetActive(true);
        fourthScreen.GetComponentInChildren<TMP_Text>().text = "Projekt:\n" + ES3.Load("NAME");
    }

    public void CreateCircle()
    {
        secondScreen.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void SaveName()
    {
        string name = planetName.text;
        ES3.Save("NAME", name);

        if (string.IsNullOrEmpty(name))
        {
            thirdScreen.transform.GetChild(2).gameObject.SetActive(false);
        } else
        {
            thirdScreen.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        StartCoroutine(LoadingScreen(4));
    }

    IEnumerator LoadingScreen(int scene)
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Single)); //Hauptszene
        scenesToLoad.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive)); // lädt Index Welt in Haupszene

        for (int i=0; i<scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                float progress = 0f;
                progress += Mathf.Clamp01(scenesToLoad[i].progress);
                asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
                for (int j = 0; j < asteroids.Length; j++)
                {
                    asteroids[j].transform.localScale = new Vector3(0.3f + progress, 0.3f + progress, 0.3f + progress);
                }
                yield return null;
            }
        }
    }

    public void DeleteOnFingerUp(GameObject DeleteThis)
    {
        StartCoroutine(FadeThis(DeleteThis));
    }

    IEnumerator FadeThis(GameObject DeleteThis)
    {
        EverythingDeleted += 1;
        float i = 1f;
        while (i > 0)
        {
            i -= 0.05f;
            DeleteThis.GetComponent<RectTransform>().localScale = new Vector3(Mathf.MoveTowards(0, 1, i), Mathf.MoveTowards(0, 1, i), 0);
            yield return null;
        }
        Destroy(DeleteThis);

        if (EverythingDeleted == 3)
        {
            secondScreen.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void SkipCreationSetDefault()
    {
        if (GameObject.FindGameObjectWithTag("Variables"))
        {
            Variables.Instance.Started = DateTime.Now;
            Variables.Instance.lastClosed = DateTime.Now;
            Variables.Instance.currentLevelIndex = 7;
            Variables.Instance.actionHours = 0;
            Variables.Instance.actionCount = 5; //default
            Variables.Instance.historyCount = 1;
            Variables.Instance.waterUseRate = 0.4f;
            Variables.Instance.waterStorageRate = 0.5f;
            Variables.Instance.reproductionRate = 0.3f;
            Variables.Instance.water = 10000f;
            Variables.Instance.human = 1000f;
            Variables.Instance.maxWater = 20000f;
            Variables.Instance.rain = 300f;
            Variables.Instance.deaths = 0f;
            Variables.Instance.s = 0f;
            Variables.Instance.valuesSet = false;
        }

        ES3.Save("NAME", "Planet");
        ES3.Save("LIFE", "Leben");
        ES3.Save("HC", 1);
        ES3.Save("WATER", 10000f);
        ES3.Save("Property_maxWater", 20000f);
        ES3.Save("HUMAN", 1000f);
        ES3.Save("RAIN", 300f);
        ES3.Save("Property_actionHours", 0);
        ES3.Save("Property_actionCounter", 0);
        ES3.Save("Property_stateCount", 0f);
        ES3.Save("StartedAt", DateTime.Now);
        ES3.Save("GameOver", false);
        ES3.Save("CloudNeedsCreation", true);
        ES3.DeleteFile("history.csv");
        ES3.DeleteKey("VALUES");
        ES3.DeleteKey("randomEventTimer");
        ES3.DeleteKey("randomEventSurvived");
        ES3.DeleteKey("LifeLine");

        ES3.Save("CLI", 7);

        StartCoroutine(LoadingScreenSkip(7));
    }

    IEnumerator LoadingScreenSkip(int scene)
    {
        skipLoadingAnim.gameObject.SetActive(true);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Single)); //Hauptszene
        scenesToLoad.Add(SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive)); // lädt Index Welt in Haupszene

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                float progress = 0f;
                progress += Mathf.Clamp01(scenesToLoad[i].progress);
                skipLoadingAnim.value = progress;
                yield return null;
            }
        }
    }
}