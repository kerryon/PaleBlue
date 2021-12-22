using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public SetupUI UI;
    private bool isInteractable;

    void Start()
    {
        isInteractable = false;
        StartCoroutine(ActivateInteraction());
    }

    void Update()
    {
        if (isInteractable && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(FadeTitle());
            }
            if (touch.phase == TouchPhase.Ended)
            {
                StartCoroutine(DestroyTitle());
                UI.OpenUI();
            }
        }

#if UNITY_EDITOR
        if (isInteractable && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeTitle());
            StartCoroutine(DestroyTitle());
            UI.OpenUI();
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
