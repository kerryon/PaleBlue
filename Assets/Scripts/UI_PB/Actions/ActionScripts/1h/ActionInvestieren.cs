using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionInvestieren : MonoBehaviour
{
    public TMP_Text _changeText;
    public Scrollbar scrollbar;

    public void Invest()
    {
        _changeText.text = "<font=Fonts/Config-Bold><size=180%>" + (int)Mathf.Lerp(0, 10, scrollbar.value) + "</size></font><color=#609AFFE6> Std.</color>";
    }

    public void ExitAction()
    {
        // DO SOMETHING HERE
        transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject, 1.2f);
    }
}
