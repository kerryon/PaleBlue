using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionForschen : MonoBehaviour
{
    public GameObject result;
    public GameObject startUI;
    public GameObject exitUI;

    public void Research()
    {
        int randomField = Random.Range(0, 9);

        switch(randomField)
        {
            case 0:
                Variables.Instance.h_conflict -= 20000f;
                StartCoroutine(WaitForResult(0.ToString()));
                break;
            case 1:
                Variables.Instance.h_luxury -= 20000f;
                StartCoroutine(WaitForResult(1.ToString()));
                break;
            case 2:
                Variables.Instance.h_industry -= 20000f;
                StartCoroutine(WaitForResult(2.ToString()));
                break;
            case 3:
                Variables.Instance.h_agriculture -= 20000f;
                StartCoroutine(WaitForResult(3.ToString()));
                break;
            case 4:
                Variables.Instance.h_waste -= 20000f;
                StartCoroutine(WaitForResult(4.ToString()));
                break;
            case 5:
                Variables.Instance.h_urbanisation -= 20000f;
                StartCoroutine(WaitForResult(5.ToString()));
                break;
            case 6:
                Variables.Instance.h_energy -= 20000f;
                StartCoroutine(WaitForResult(6.ToString()));
                break;
            case 7:
                Variables.Instance.h_overfishing -= 20000f;
                StartCoroutine(WaitForResult(7.ToString()));
                break;
            case 8:
                Variables.Instance.h_wasteWater -= 20000f;
                StartCoroutine(WaitForResult(8.ToString()));
                break;
            case 9:
                Variables.Instance.h_waterStructure -= 20000f;
                StartCoroutine(WaitForResult(9.ToString()));
                break;
        }
    }

    IEnumerator WaitForResult(string text)
    {
        startUI.SetActive(false);

        yield return new WaitForSeconds(3);

        exitUI.SetActive(true);
        result.SetActive(true);
        result.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void ExitAction()
    {
        GetComponentInParent<ActionList>().DestroyAction();
    }
}
