using System.Collections;
using UnityEngine;

public class UIInitiate : MonoBehaviour
{
    public GameObject UI;
    public GameObject nextPhase;

    void Awake()
    {
        if (UI)
        {
            UI.SetActive(true);
            UI.GetComponent<UI>().OpenUI();
        }
    }

    public void NextPhase()
    {
        if (nextPhase)
        {
            StartCoroutine(PhaseTransition());
        }
    }

    IEnumerator PhaseTransition()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("PhaseTransition");

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        nextPhase.SetActive(true);
    }
}
