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
        if (Variables.Instance.actionHours < value)
        {
            StartCoroutine(GetComponentInParent<ActionList>().ToggleNotification());
        }
        else
        {
            Variables.Instance.actionHours -= value;
            GetComponentInParent<ActionList>().DestroyAction();
        }
        Variables.Instance.maxWater += slider.value * 1000;

    }
}
