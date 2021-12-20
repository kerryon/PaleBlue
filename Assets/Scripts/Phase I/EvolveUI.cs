using System.Collections;
using UnityEngine;

public class EvolveUI : MonoBehaviour
{
    public Camera UICamera;
    LevelLoader levelLoader;
    public GameObject EnableZoomDrag;

    private bool cloudNeedsCreation;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        cloudNeedsCreation = ES3.Load("CloudNeedsCreation", true);
        OpenUI();
    }

    public void OpenUI()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelLoader.AttachCam(UICamera);

        if (cloudNeedsCreation)
        {
            StartCoroutine(ShowUI());
        }
        else
        {
            EnableZoomDrag.SetActive(true);
        }
    }

    private IEnumerator ShowUI()
    {
        cloudNeedsCreation = false;
        ES3.Save("CloudNeedsCreation", cloudNeedsCreation);

        yield return new WaitForSeconds(1);

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
