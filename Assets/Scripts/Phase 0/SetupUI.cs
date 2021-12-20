using System.Collections;
using UnityEngine;

public class SetupUI : MonoBehaviour
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

    private FAB iPer, iMin, iMax;

    public GameObject planet;
    public Animator habitableZone;

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
        StartCoroutine(OpenInterface());

        isShown = false;
        UpDown1 = true;
        UpDown2 = true;

        if (Instructions_perf)
        {
            iPer = Instructions_perf.GetComponent<FAB>();
        }
        if (Instructions_min)
        {
            iMin = Instructions_min.GetComponent<FAB>();
        }
        if (Instructions_max)
        {
            iMax = Instructions_max.GetComponent<FAB>();
        }
    }

    IEnumerator OpenInterface()
    {
        if (Variables.Instance.historyCount != 1)
        {
            yield return new WaitForSeconds(0.5f);
            OpenUI();
        }
    }

    public void OpenUI()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>();
        levelLoader.AttachCam(UICamera);
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(0.5f);
        Instructions_info.SetActive(true);
    }

    void Update()
    {
        if (Phase1.activeSelf)
        {
            StartCoroutine(FirstPhase());
        } else

        if (Phase2.activeSelf)
        {
            StartCoroutine(SecondPhase());
        } else

        if (Phase3.activeSelf)
        {
            StartCoroutine(ThirdPhase());
        }
    }

    IEnumerator FirstPhase()
    {
        if (planet.transform.localScale.x < 97 && planet.transform.localScale.x > 45 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                iPer.FABFadeOutNext(2);
            }
            UpDown1 = true;
            isShown = false;
        }

        if (planet.transform.localScale.x >= 97 && planet.transform.localScale.x <= 103 && !isShown)
        {
            if (UpDown1)
            {
                    iMin.FABFadeOutNext(3);
                    UpDown1 = false;
            }
            else
            {
                    iMax.FABFadeOutNext(3);
                    UpDown1 = true;
            }

            isShown = true;
        }

        if (planet.transform.localScale.x > 103 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                iPer.FABFadeOutNext(4);
            }
            UpDown1 = false;
            isShown = false;
        }
    }

    private IEnumerator SecondPhase()
    {
        if (planet.transform.position.y > -220 && isShown)
        {

        habitableZone.SetTrigger("zoneExit");

            if (!Instructions_perf.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                iPer.FABFadeOutNext(1);
            }
            UpDown2 = true;
            isShown = false;
        }

        if (planet.transform.position.y < -220 && planet.transform.position.y > -240 && !isShown)
        {
            habitableZone.SetTrigger("zoneEntered");

            if (UpDown2)
            {
                if (!Instructions_min.activeSelf)
                {
                    yield return new WaitForSeconds(1);
                }
                iMin.FABFadeOutNext(2);
                UpDown2 = false;
            }
            else
            {
                if (!Instructions_max.activeSelf)
                {
                    yield return new WaitForSeconds(1);
                }
                iMax.FABFadeOutNext(2);
                UpDown2 = true;
            }
            isShown = true;
        }

        if (planet.transform.position.y < -240 && isShown)
        {
            habitableZone.SetTrigger("zoneExit");

            if (!Instructions_perf.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                iPer.FABFadeOutNext(3);
            }
            UpDown2 = false;
            isShown = false;
        }
    }

    private IEnumerator ThirdPhase()
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
            iMin.FABFadeOutNext(2);
            isShown = true;
            yield return null;
        }

        if (GameObject.FindGameObjectsWithTag("Asteroid").Length <= 45 && isShown)
        {
            if (!Instructions_perf.activeSelf)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                iPer.FABFadeOutNext(3);
                isShown = false;
            }
        }
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
        }
        else if (number == 2)
        {
            Instructions_max.SetActive(true);
            Instructions_perf.SetActive(false);
            Instructions_min.SetActive(false);

        }
        else
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
}
