using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private Vector2 panelLocationT;
    private Vector2 panelLocationB;
    public RectTransform canvasScale;
    public RectTransform rt;
    public RectTransform rtBG;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;
    private int currentPage = 1;

    public GameObject swipeIndicator;

    void Start()
    {
        panelLocationT = rt.offsetMin;
        panelLocationB = rt.offsetMax;
    }

    void Update()
    {
        if (totalPages == 3)
        {
            rtBG.sizeDelta = new Vector2(0, canvasScale.sizeDelta.y * 2);
        } else
        {
            rtBG.sizeDelta = new Vector2(0, canvasScale.sizeDelta.y);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.y - data.position.y;
        rt.offsetMin = panelLocationT - new Vector2(0, difference);
        rt.offsetMax = panelLocationB - new Vector2(0, difference * -1);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.y - data.position.y) / canvasScale.sizeDelta.y;
        
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector2 newLocationT = panelLocationT;
            Vector2 newLocationB = panelLocationB;
            if (percentage < 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocationT += new Vector2(0, canvasScale.sizeDelta.y);
                newLocationB += new Vector2(0, -canvasScale.sizeDelta.y);
            }
            else if (percentage > 0 && currentPage > 1)
            {
                currentPage--;
                newLocationT += new Vector2(0, -canvasScale.sizeDelta.y);
                newLocationB += new Vector2(0, canvasScale.sizeDelta.y);
            }
            StartCoroutine(SmoothMove(rt.offsetMin, rt.offsetMax, newLocationT, newLocationB, easing));
            panelLocationT = newLocationT;
            panelLocationB = newLocationB;
        }
        else
        {
            StartCoroutine(SmoothMove(rt.offsetMin, rt.offsetMax, panelLocationT, panelLocationB, easing));
        }

        Destroy(swipeIndicator);
    }

    public void OnTapClose()
    {
        Vector2 newLocationT = panelLocationT;
        Vector2 newLocationB = panelLocationB;

            currentPage--;
            newLocationT += new Vector2(0, -canvasScale.sizeDelta.y);
            newLocationB += new Vector2(0, canvasScale.sizeDelta.y);
        
        StartCoroutine(SmoothMove(rt.offsetMin, rt.offsetMax, newLocationT, newLocationB, easing));
        panelLocationT = newLocationT;
        panelLocationB = newLocationB;
    }

    IEnumerator SmoothMove(Vector2 startposT, Vector2 startposB, Vector2 endposT, Vector2 endposB, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            rt.offsetMin = Vector2.Lerp(startposT, endposT, Mathf.SmoothStep(0f, 1f, t));
            rt.offsetMax = Vector2.Lerp(startposB, endposB, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}