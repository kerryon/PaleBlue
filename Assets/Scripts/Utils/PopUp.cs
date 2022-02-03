using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Animator animatorFAB;
    public Animator animatorTXT;
    public Animator animatorInfoPopup;
    private Animator animatorPopup;

    [Header("Scriptable Objects")]
    public ScriptableObjectContent[] popUpContent;

    public TMP_Text description;

    public TMP_Text FABText;
    public TMP_Text FABInfoTitle;
    public TMP_Text FABInfo;
    public Button button;

    public Image titleImage;

    void Start()
    {
        gameObject.SetActive(false);
        animatorPopup = gameObject.GetComponent<Animator>();

        if (GetComponent<Canvas>().worldCamera == null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        }
    }

    public void PopupTriggerTouched()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void FadeOutPopup()
    {
        StartCoroutine(FadingPopup());
    }

    IEnumerator FadingPopup()
    {
        animatorTXT.SetTrigger("FABTextTrigger");
        animatorFAB.SetTrigger("FABTrigger");
        animatorInfoPopup.SetTrigger("FABInfoTrigger");
        animatorPopup.SetTrigger("PopUpTrigger");
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
