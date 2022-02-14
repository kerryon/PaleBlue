using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionInvestieren : MonoBehaviour
{
    public TMP_Text _changeText;
    public Slider slider;

    public void Invest()
    {
        _changeText.text = "<font=Fonts/Config-Bold><size=180%>" + slider.value + "</size></font><color=#609AFFE6> Std.</color>";
    }

    public void ExitAction()
    {
        int value = (int)slider.value;
        Variables.Instance.actionHours -= value;
        Variables.Instance.maxWater += slider.value * 1000;

        GetComponentInParent<ActionList>().DestroyAction();
    }
}
