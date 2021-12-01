using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject Buttons;
    private bool isInteractable;

    void Start()
    {
        if (Variables.Instance.historyCount != 10)
        {
            Destroy(gameObject);
        }
        isInteractable = false;
        StartCoroutine(ActivateInteraction());
    }

    void Update()
    {
        if (isInteractable == true && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(FadeTitle());
            }
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(DestroyTitle());
                Buttons.SetActive(true);
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeTitle());
            StartCoroutine(DestroyTitle());
            Buttons.SetActive(true);
        }
#endif
    }

    IEnumerator ActivateInteraction()
    {
        yield return new WaitForSeconds(12);
        isInteractable = true;
    }

    IEnumerator FadeTitle()
    {
        Animator titleText = gameObject.GetComponent<Animator>();
        titleText.SetTrigger("FadeTitleTrigger");
        yield return new WaitForSeconds(1);
    }

    IEnumerator DestroyTitle()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
