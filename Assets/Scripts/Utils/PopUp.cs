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

    public Animator buttons;

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
            if (buttons)
            {
                buttons.SetTrigger("ButtonsDisabled");
                buttons.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("FABTrigger");
                StartCoroutine(ActionsSetActive());
            }
        }
    }

    IEnumerator ActionsSetActive()
    {
        yield return new WaitForSeconds(1);
        buttons.gameObject.transform.GetChild(0).gameObject.SetActive(false);
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

        if (buttons)
        {
            yield return new WaitForSeconds(0.2f);
            buttons.SetTrigger("ButtonsEnabled");
            buttons.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
