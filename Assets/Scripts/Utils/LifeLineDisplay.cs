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

    void Start()
    {
        RectTransform parentCanvas = transform.parent.parent.GetComponent<RectTransform>();
        lineLength = parentCanvas.rect.width * parentCanvas.GetComponent<CanvasScaler>().scaleFactor - 140;

        ll = ES3.Load("LifeLine", ll);
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = ll.Count + 1;

        var points = new Vector3[ll.Count + 1];

        points[0] = new Vector3(0, 0, 0.0f);
        for (int i = 0; i < ll.Count; i++)
        {
            float x = (i+1) * (lineLength / ll.Count);
            points[i+1] = new Vector3(x, Map(ll[i], 0f, ll.Max(), 0f, 300f), 0.0f);
            Debug.Log(points[i+1].x);
        }
        lr.SetPositions(points);
    }

    private float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }
}
