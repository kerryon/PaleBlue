using System.Collections;
using UnityEngine;

public class EvolveUI : MonoBehaviour
{
    public Camera UICamera;
    LevelLoader levelLoader;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        if (Variables.Instance.currentLevelIndex >= 3)
        {
            StartCoroutine(OpenInterface());
        }
    }

    IEnumerator OpenInterface()
    {
        yield return new WaitForSeconds(7);
        OpenUI();
    }

    public void OpenUI()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelLoader.attachCam(UICamera);
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(3);

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
