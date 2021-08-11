using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

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

    public int collisionCount = 0;

    private int EverythingDeleted = 0;

    public void Awake()
    {
        secondScreen.gameObject.SetActive(false);
        thirdScreen.gameObject.SetActive(false);
        fourthScreen.gameObject.SetActive(false);
    }

    public void ScreenTwo()
    {
        firstScreen.gameObject.SetActive(false);
        secondScreen.gameObject.SetActive(true);
    }

    public void ScreenThree()
    {
        secondScreen.gameObject.SetActive(false);
        thirdScreen.gameObject.SetActive(true);
    }

    public void ScreenFour()
    {
        thirdScreen.gameObject.SetActive(false);
        fourthScreen.gameObject.SetActive(true);
        meteorSpawner.gameObject.SetActive(true);
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

        if (String.IsNullOrEmpty(name))
        {
            thirdScreen.transform.GetChild(3).gameObject.SetActive(false);
        } else
        {
            thirdScreen.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Single));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive)); // lädt erste Welt in Haupszene

        for (int i=0; i<scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                float progress = 1f;
                progress -= Mathf.Clamp01(scenesToLoad[i].progress);
                loadingAnim.transform.localScale = new Vector3(progress, progress, progress);
                //loadingAnimTxt.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, progress);
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
        DeleteThis.gameObject.SetActive(false);

        if (EverythingDeleted == 3)
        {
            secondScreen.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}