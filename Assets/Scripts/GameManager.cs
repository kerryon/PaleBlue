using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Gui;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Material introMat;
    public GameObject infoWindow;
    public LeanButton continueButton;
    public GameObject newGame;
    public GameObject confirmGame;

    float fade = 0f;
    float duration = 7.0f;
    float startTime;
    private int currentLevelIndex;

    void Start()
    {
        infoWindow.SetActive(false);
        confirmGame.SetActive(false);
        introMat.SetFloat("_WobbleThreshold", fade);

        currentLevelIndex = ES3.Load("CLI", 3);

        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        fade = Mathf.SmoothStep(0.7f, 3f, t);
        introMat.SetFloat("_WobbleThreshold", fade);

        if (currentLevelIndex <= 3)
        {
            continueButton.interactable = false;
            continueButton.GetComponentInChildren<TMP_Text>().color = new Color32(0, 0, 0, 70);

        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void NewGame()
    {
        if (currentLevelIndex > 3)
        {
            newGame.SetActive(false);
            confirmGame.SetActive(true);
            StartCoroutine(ConfirmationTimer());
        } else
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            ES3.Save("CLI", 3);
            ES3.Save("StartedAt", DateTime.Now);
        }
    }

    IEnumerator ConfirmationTimer()
    {
        yield return new WaitForSeconds(10);
        confirmGame.SetActive(false);
        newGame.SetActive(true);
    }

    public void ConfirmNewGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        ES3.Save("CLI", 3);
    }

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void ContinueGame()
    {
        StartCoroutine(LoadCurrentScene());
    }

    IEnumerator LoadCurrentScene()
    {
        scenesToLoad.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Single));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(currentLevelIndex, LoadSceneMode.Additive));
        continueButton.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                float grow = 2f;
                grow -= Mathf.Clamp01(scenesToLoad[i].progress) * 2;
                introMat.SetFloat("_WobbleThreshold", grow);
                yield return null;
            }
        }
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
