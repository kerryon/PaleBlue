using UnityEngine;
using System.Collections;

public class FAB : MonoBehaviour
{
    public Animator animatorButton;
    public Animator animatorText;
    public Animator animatorInfo;
    public GameObject interactionModule;

    public void FABFadeOutNext(int nextScene)
    {
        StartCoroutine(FadeToNext(nextScene));
    }

    IEnumerator FadeToNext(int nextScene)
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

        if (nextScene != 10)
        {
            GameObject nextGameObject = transform.parent.GetChild(nextScene).gameObject;

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
