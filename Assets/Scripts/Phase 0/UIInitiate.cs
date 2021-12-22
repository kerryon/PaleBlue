using System.Collections;
using UnityEngine;

public class UIInitiate : MonoBehaviour
{
    public GameObject UI;
    public GameObject nextPhase;
    public Material planetMat;

    Menu _menu;

    void Awake()
    {
        if (UI)
        {
            UI.SetActive(true);
        }
    }

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
        if (planetMat)
        {
            planetMat.color = Color.white;
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
        _menu.AppendHistory(Variables.Instance.historyCount);
        gameObject.SetActive(false);
        nextPhase.SetActive(true);
    }
}
