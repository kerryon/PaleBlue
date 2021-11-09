using System.Collections;
using UnityEngine;

public class UIInitiate : MonoBehaviour
{
    public GameObject UI;
    public GameObject nextPhase;

    Menu menu;

    void Awake()
    {
        if (UI)
        {
            UI.SetActive(true);
        }
    }

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
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
        menu.NewScene();
        gameObject.SetActive(false);
        nextPhase.SetActive(true);
    }
}
