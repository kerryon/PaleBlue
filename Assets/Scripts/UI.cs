using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Camera UICamera;
    public GameObject Instructions_info;
    public GameObject Instructions_min;
    public GameObject Instructions_perf;
    public GameObject Instructions_max;

    public GameObject Phase1;
    public GameObject Phase2;
    public GameObject Phase3;
    public GameObject Phase4;

    private bool isShown;
    private bool UpDown1;
    private bool UpDown2;

    public GameObject planet;

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
        int currentLevelIndex = ES3.Load("CLI", 3);
        if (currentLevelIndex > 4)
        {
           StartCoroutine(OpenInterface());
        }

        isShown = false;
        UpDown1 = true;
        UpDown2 = true;
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

        Instructions_info.gameObject.SetActive(true);
    }

    public void PhaseFourTrigger(int number)
    {
        if (Instructions_info.activeSelf)
        {
            Instructions_info.SetActive(false);
        }
        if (number == 1)
        {
            Instructions_min.SetActive(true);
            Instructions_max.SetActive(false);
            Instructions_perf.SetActive(false);
        } else if (number == 2)
        {
            Instructions_max.SetActive(true);
            Instructions_perf.SetActive(false);
            Instructions_min.SetActive(false);

        } else
        {
            Instructions_perf.SetActive(true);
            Instructions_min.SetActive(false);
            Instructions_max.SetActive(false);
        }
    }

    public void PhaseFourDeselectTrigger(int number)
    {
        if (number == 1)
        {
            Instructions_min.SetActive(false);
        }
        else if (number == 2)
        {
            Instructions_max.SetActive(false);
        }
        else
        {
            Instructions_perf.SetActive(false);
        }
    }

    void Update()
    {
        if (Phase1.activeSelf)
        {
            StartCoroutine(FirstPhase());
        } else

        if (Phase2.activeSelf)
        {
            StartCoroutine(secondPhase());
        } else

        if (Phase3.activeSelf)
        {
            StartCoroutine(thirdPhase());
        }
    }

    IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator FirstPhase()
    {
        if (planet.transform.localScale.x < 97 && planet.transform.localScale.x > 45 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                StartCoroutine(WaitSomeTime());
            }
            else
            {
                Instructions_perf.GetComponent<FAB>().FABFadeOutNext(2);
                isShown = false;
            }
            yield return null;
        }

        if (planet.transform.localScale.x >= 97 && planet.transform.localScale.x <= 103 && !isShown)
        {
            if (UpDown1)
            {
                Instructions_min.GetComponent<FAB>().FABFadeOutNext(3);
                UpDown1 = false;
            }
            else
            {
                Instructions_max.GetComponent<FAB>().FABFadeOutNext(3);
                UpDown1 = true;
            }

            isShown = true;
            yield return null;
        }

        if (planet.transform.localScale.x > 103 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                StartCoroutine(WaitSomeTime());
            }
            else
            {
                Instructions_perf.GetComponent<FAB>().FABFadeOutNext(4);
                isShown = false;
            }
        }
        yield return null;
    }

    IEnumerator secondPhase()
    {
        if (planet.transform.position.y > 0 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                StartCoroutine(WaitSomeTime());
            }
            else
            {
                Instructions_perf.GetComponent<FAB>().FABFadeOutNext(1);
                isShown = false;
            }
            yield return null;
        }

        if (planet.transform.position.y < 0 && planet.transform.position.y > -35 && !isShown)
        {
            if (UpDown2)
            {
                Instructions_min.GetComponent<FAB>().FABFadeOutNext(2);
                UpDown2 = false;
            }
            else
            {
                Instructions_max.GetComponent<FAB>().FABFadeOutNext(2);
                UpDown2 = true;
            }
            isShown = true;
            yield return null;
        }

        if (planet.transform.position.y < -35 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                StartCoroutine(WaitSomeTime());
            }
            else
            {
                Instructions_perf.GetComponent<FAB>().FABFadeOutNext(3);
                isShown = false;
            }
            yield return null;
        }
    }

    IEnumerator thirdPhase()
    {
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length > 55)
        {
            isShown = true;
            yield return null;
        }
        if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 55)
        {
            isShown = false;
            yield return null;
        }

        if (GameObject.FindGameObjectsWithTag("Asteroid").Length < 55 && GameObject.FindGameObjectsWithTag("Asteroid").Length > 45 && !isShown)
        {
            Instructions_min.GetComponent<FAB>().FABFadeOutNext(2);
            isShown = true;
            yield return null;
        }

        if (GameObject.FindGameObjectsWithTag("Asteroid").Length <= 45 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                StartCoroutine(WaitSomeTime());
            }
            else
            {
                Instructions_perf.GetComponent<FAB>().FABFadeOutNext(3);
                isShown = false;
            }
            yield return null;
        }
    }
}
