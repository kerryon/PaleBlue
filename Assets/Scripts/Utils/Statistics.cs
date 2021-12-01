using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public TMP_Text waterDisplay;
    public TMP_Text humanDisplay;
    public Image waterRatio;
    public Image humanRatio;
    private float _ratio;

    void Start()
    {

    }

    void Update()
    {
        waterDisplay.text = Variables.Instance.water + "\n<font=fonts/Config-Light><size=40%>Nutzbares Wasser</size></font>";
        humanDisplay.text = (int)Variables.Instance.human + "\n<font=fonts/Config-Light><size=40%>Individuen</size></font>";

        _ratio = Variables.Instance.waterUseRate * Variables.Instance.human / Variables.Instance.water;
        waterRatio.fillAmount = 1 - _ratio;
        humanRatio.fillAmount = _ratio;
    }

    private float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
