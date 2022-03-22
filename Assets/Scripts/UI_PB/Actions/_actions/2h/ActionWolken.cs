using System.Collections;
using UnityEngine;
using SpaceGraphicsToolkit.Cloudsphere;

public class ActionWolken : MonoBehaviour
{
    public GameObject EnableDragZoom;
    public GameObject ShapeDetector;

    void Start()
    {
        StartCoroutine(EnableShapeDetector());
    }

    IEnumerator EnableShapeDetector()
    {
        yield return new WaitForSeconds(0.1f);
        ShapeDetector.SetActive(true);
    }

    public void AddRain()
    {
        Variables.Instance.rain += 300f;
        transform.parent.parent.transform.GetChild(0).gameObject.SetActive(true);
        EnableDragZoom.SetActive(true);

        StartCoroutine(CloudAnimation());
    }

    IEnumerator CloudAnimation()
    {
        float sec = 1f;
        CanvasGroup _canvas = transform.GetChild(0).GetComponent<CanvasGroup>();
        SgtCloudsphere _clouds = GameObject.FindGameObjectWithTag("Clouds").GetComponent<SgtCloudsphere>();
        
        float _cloudsBrightness = _clouds.Brightness;

        while (sec > 0f)
        {
            _clouds.Brightness = Mathf.Lerp(1f, _cloudsBrightness, sec);
            _canvas.alpha = sec;

            sec -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject, 1.2f);
    }
}
