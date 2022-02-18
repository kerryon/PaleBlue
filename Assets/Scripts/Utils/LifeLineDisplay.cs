using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LifeLineDisplay : MonoBehaviour
{
    private LineRenderer lr;
    private float lineLength;
    List<float> ll = new List<float>();
    List<int> dayInstances = new List<int>();
    private int total;

    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    private readonly Color32[] gradientColors = {new Color32(32, 32, 32 ,255), new Color32(96, 154, 255, 255), new Color32(255, 96, 96, 255), new Color32(32, 32, 32, 255), new Color32(96, 154, 255, 255), new Color32(255, 96, 96, 255), new Color32(32, 32, 32, 255) };

    void Start()
    {
        RectTransform parentCanvas = transform.parent.parent.GetComponent<RectTransform>();
        lineLength = parentCanvas.rect.width * parentCanvas.GetComponent<CanvasScaler>().scaleFactor - 140;

        ll = ES3.Load("LifeLine", ll);
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = ll.Count + 1;

        gradient = new Gradient();
        colorKey = new GradientColorKey[7];
        alphaKey = new GradientAlphaKey[7];

        var points = new Vector3[ll.Count + 1];

        var sheet = new ES3Spreadsheet();
        sheet.Load("history.csv");

        points[0] = new Vector3(0, 0, 0.0f);
        for (int i = 0; i < ll.Count; i++)
        {
            float x = (i + 1) * (lineLength / ll.Count);
            points[i + 1] = new Vector3(x, Map(ll[i], 0f, ll.Max(), 0f, 300f), 0.0f);
        }

        for (int i = 0; i < Variables.Instance.historyCount; i++)
        {
            dayInstances.Add(sheet.GetCell<int>(2, i));
        }

        for (int i = 0; i < 7; i++)
        {
            total += dayInstances.Count(n => n == i);

            colorKey[i].color = gradientColors[i];
            colorKey[i].time = Mathf.InverseLerp(0, Variables.Instance.historyCount, total);

            alphaKey[i].alpha = 1.0f;
            alphaKey[i].time = Mathf.InverseLerp(0, Variables.Instance.historyCount, total);
        }
        gradient.SetKeys(colorKey, alphaKey);
        gradient.mode = GradientMode.Fixed;

        lr.SetPositions(points);
        lr.colorGradient = gradient;
    }

    private float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
