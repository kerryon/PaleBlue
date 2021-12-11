using System.Collections;
using UnityEngine;

public class ActionWolken : MonoBehaviour
{
    public GameObject EnableDragZoom;

    public void AddRain()
    {
        Variables.Instance.rain += 300f;
        transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
        EnableDragZoom.SetActive(true);
        StartCoroutine(CloseAction());
    }

    IEnumerator CloseAction()
    {
        float sec = 1f;
        CanvasGroup _canvas = transform.GetChild(0).GetComponent<CanvasGroup>();

        while (sec > 0f)
        {
            _canvas.alpha = sec;
            sec -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
