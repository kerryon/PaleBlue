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

    private List<Image> imgWater = new List<Image>();
    private List<Image> imgHuman = new List<Image>();

    private float _ratio;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            imgWater.Add(waterRatio.transform.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < 4; i++)
        {
            imgHuman.Add(humanRatio.transform.GetChild(i).GetComponent<Image>());
        }
    }

    void Update()
    {
        waterDisplay.text = Variables.Instance.water.ToString("n0").Replace(",", ".") + "\n<font=fonts/Config-Light><size=40%>Nutzbare Wasser Kapazität</size></font>";
        humanDisplay.text = Variables.Instance.human.ToString("n0").Replace(",", ".") + "\n<font=fonts/Config-Light><size=40%>Individuen</size></font>";

        _ratio = Variables.Instance.waterUseRate * Variables.Instance.human / Variables.Instance.water;
        waterRatio.fillAmount = 1 - _ratio;
        humanRatio.fillAmount = _ratio;

        imgWater[0].fillAmount = Variables.Instance.waterEcology;
        imgWater[1].fillAmount = Variables.Instance.waterQuality;
        imgWater[2].fillAmount = Variables.Instance.waterQuantity;
        imgWater[3].fillAmount = Variables.Instance.waterSealevel;

        imgHuman[0].fillAmount = Mathf.InverseLerp(0f, 7f, Variables.Instance.s);
        imgHuman[1].fillAmount = Mathf.InverseLerp(0f, 8f, Variables.Instance.e);
        imgHuman[2].fillAmount = Variables.Instance.w;
        imgHuman[3].fillAmount = 1 - Variables.Instance.c;
    }
}
