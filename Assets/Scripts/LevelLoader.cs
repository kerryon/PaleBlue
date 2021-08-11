using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Camera mainCamera;
    public Camera menuCamera;

    [Header("Parameter")]
    public int water;
    public int human;
    public int currentLevelIndex;

    void Start()
    {
        currentLevelIndex = ES3.Load("CLI", 3);
        water = ES3.Load("WATER", 0);
        human = ES3.Load("HUMAN", 0);
    }

    void Update()
    {
        
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            ES3.Save("CLI", currentLevelIndex);
            ES3.Save("WATER", water);
            ES3.Save("HUMAN", human);
        }
    }

    void OnApplicationQuit()
    {
        ES3.Save("CLI", currentLevelIndex);
        ES3.Save("WATER", water);
        ES3.Save("HUMAN", human);
    }

    public void LoadNextLvl()
    {
        StartCoroutine(LoadLvl(currentLevelIndex));
        LevelCounter();
    }

    List<AsyncOperation> scenesToChange = new List<AsyncOperation>();

    IEnumerator LoadLvl(int levelIndex)
    {
        transition.SetTrigger("LevelLoadStart");

        yield return new WaitForSeconds(1);

        scenesToChange.Add(SceneManager.UnloadSceneAsync(levelIndex));
        scenesToChange.Add(SceneManager.LoadSceneAsync(levelIndex + 1, LoadSceneMode.Additive));

        for (int i = 0; i < scenesToChange.Count; i++)
        {
            while (!scenesToChange[i].isDone)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        transition.SetTrigger("LevelLoadEnd");
    }

    public void attachCam(Camera newUICam)
    {
        var cameraData = mainCamera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Clear();
        cameraData.cameraStack.Add(newUICam);
        cameraData.cameraStack.Add(menuCamera);
    }

    public void LevelCounter()
    {
        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            currentLevelIndex += 1;
            ES3.Save("CLI", currentLevelIndex);
        }
        else
        {
            currentLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
        }
    }
}
