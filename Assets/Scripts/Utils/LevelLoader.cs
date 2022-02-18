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
        if (Variables.Instance.currentLevelIndex < 8)
        {
            StartCoroutine(LoadLevel(Variables.Instance.currentLevelIndex));
        }
        else if (Variables.Instance.currentLevelIndex == 8)
        {
            StartCoroutine(LoadLevelReverse(Variables.Instance.currentLevelIndex));
        }
    }

    List<AsyncOperation> scenesToChange = new List<AsyncOperation>();

    IEnumerator LoadLevel(int levelIndex)
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

        if (Variables.Instance.currentLevelIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            Variables.Instance.currentLevelIndex++;
        }
        else
        {
            Variables.Instance.currentLevelIndex = SceneManager.sceneCountInBuildSettings - 1;
        }

        transition.SetTrigger("LevelLoadEnd");
    }

    public void AttachCam(Camera newUICam)
    {
        var cameraData = mainCamera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Clear();
        cameraData.cameraStack.Add(newUICam);
        cameraData.cameraStack.Add(menuCamera);
    }

    IEnumerator LoadLevelReverse(int levelIndex)
    {
        yield return new WaitForSeconds(3);

        transition.SetTrigger("LevelLoadStart");

        yield return new WaitForSeconds(1);

        scenesToChange.Add(SceneManager.UnloadSceneAsync(levelIndex));
        scenesToChange.Add(SceneManager.LoadSceneAsync(levelIndex - 1, LoadSceneMode.Additive));

        for (int i = 0; i < scenesToChange.Count; i++)
        {
            while (!scenesToChange[i].isDone)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        if (Variables.Instance.currentLevelIndex > 0)
        {
            Variables.Instance.currentLevelIndex--;
        }

        transition.SetTrigger("LevelLoadEnd");
    }
}
