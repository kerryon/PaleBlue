using UnityEngine;
using System.Collections;

public class FAB : MonoBehaviour
{
    public Animator animatorButton;
    public Animator animatorText;
    public Animator animatorInfo;
    public GameObject interactionModule;

    public void FABFadeOutNext(int number)
    {
            StartCoroutine(FadeToNext(number));
    }

    IEnumerator FadeToNext(int number)
    {
        if (animatorButton)
        {
            animatorButton.SetTrigger("FABTrigger");
        }
        animatorText.SetTrigger("FABTextTrigger");

        if (animatorInfo != null)
        {
            animatorInfo.SetTrigger("FABInfoTrigger");
        }

        yield return new WaitForSeconds(1);

        if (number != 10)
        {
            GameObject nextGameObject = transform.parent.GetChild(number).gameObject;

            nextGameObject.SetActive(true);
        }
    gameObject.SetActive(false);
    }

    public void InteractionModule()
    {
        if (interactionModule != null)
        {
            if (interactionModule.activeSelf == true)
            {
                interactionModule.SetActive(false);
            } else
            {
                interactionModule.SetActive(true);
            }
        }
    }
}
