using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class LevelLoader : MonoBehaviour
{
    [Header("Transition")]
    public Animator transition;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera menuCamera;

    public void LoadNextLvl()
    {
        StartCoroutine(LoadLvl(Variables.Instance.currentLevelIndex));
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
        if (Variables.Instance.currentLevelIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            Variables.Instance.currentLevelIndex++;
        }
        else
        {
            Variables.Instance.currentLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
        }
    }
}
