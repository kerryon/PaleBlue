using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    private int currentLevelIndex;
    public GameObject lens;

    void Start()
    {
        currentLevelIndex = ES3.Load("CLI", 1);

        if (currentLevelIndex <= 4)
        {
            Invoke(nameof(LoadInitial), 1f);
        }
        else if (currentLevelIndex > 4)
        {
            StartCoroutine(LoadScenes());
        }
    }

    void Update()
    {
        lens.transform.localPosition = new Vector3(0, Mathf.PingPong(Time.time*3, 50), 0);
    }

    private void LoadInitial()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    IEnumerator LoadScenes()
    {
        yield return new WaitForSeconds(1f);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(3, LoadSceneMode.Single));
        scenesToLoad.Add(SceneManager.LoadSceneAsync(currentLevelIndex, LoadSceneMode.Additive));
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                float t = Mathf.Lerp(100f, 120f, scenesToLoad[i].progress);
                lens.transform.localScale = new Vector3(t, t, t);
                yield return null;
            }
        }
    }
}
