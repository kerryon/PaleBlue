using System.Collections;
using System.Collections.Generic;
using LylekGames;
using UnityEngine;

public class ActionWerturteile : MonoBehaviour
{
    public GameObject signingArea;
    public GameObject[] situations;
    public GameObject btn;

    public void Choose(int judgement)
    {
        if (judgement == 1)
        {
            situations[1].SetActive(false);
            situations[0].transform.parent.GetChild(1).gameObject.SetActive(false);
            signingArea.SetActive(true);
        }
        else if (judgement == 2)
        {
            situations[0].SetActive(false);
            situations[0].transform.parent.GetChild(1).gameObject.SetActive(false);
            signingArea.SetActive(true);
        }
    }

    public void ExitAction()
    {
        Variables.Instance.h_conflict -= 15000;
        GetComponentInParent<ActionList>().DestroyAction();
    }

    void Update()
    {
        if (signingArea.activeSelf) {
            if (DrawScript.drawScript.drawHistory.Count > 0)
            {
                btn.SetActive(true);
            }
        }
    }
}
