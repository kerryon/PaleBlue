using System.Collections;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Animator animatorInfoPopup;
    private Animator animatorPopup;

    void Start()
    {
        gameObject.SetActive(false);
        animatorPopup = gameObject.GetComponent<Animator>();

    }

    public void PopupTriggerTouched()
    {
        gameObject.SetActive(true);
    }

    public void FadeOutPopup()
    {
        StartCoroutine(FadingPopup());
    }

    IEnumerator FadingPopup()
    {
        animatorInfoPopup.SetTrigger("FABInfoTrigger");
        animatorPopup.SetTrigger("PopUpTrigger");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
