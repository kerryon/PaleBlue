using System.Collections;
using SpaceGraphicsToolkit;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject[] events;

    private bool survived;
    private float randomFloat;
    private float totalSeconds;
    private float secondsToImpact;
    private int catastrophe;

    private readonly float minSeconds = 100000f;
    private readonly float maxSeconds = 604800f; //7 Tage

    void Start()
    {
        if (!ES3.KeyExists("randomEventTimer"))
        {
            randomFloat = Random.Range(minSeconds, maxSeconds);
            ES3.Save("randomEventTimer", randomFloat);
        }
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        totalSeconds = ES3.Load("randomEventTimer", maxSeconds / 2);
        secondsToImpact = (float)(totalSeconds - Variables.Instance.timespan.TotalSeconds);

        survived = ES3.Load("randomEventSurvived", false);

        if (!survived)
        {
            Invoke(nameof(TriggerCatastrophe), secondsToImpact);
        }
    }

    private float CurveWeightedRandom(AnimationCurve curve)
    {
        return curve.Evaluate(Random.value);
    }

    public void TriggerCatastrophe()
    {
        if (!ES3.KeyExists("randomEvent"))
        {
            catastrophe = (int)CurveWeightedRandom(curve);
            ES3.Save("randomEvent", catastrophe);
        } else
        {
            catastrophe = ES3.Load("randomEvent", 0);
        }

        GameObject e = Instantiate(events[catastrophe], gameObject.transform);
        e.name = "e_" + catastrophe;

        transform.GetChild(0).GetComponent<RandomEventHandler>().CreatePinPrefab(catastrophe);

        switch(catastrophe) // ????????
        {
            case 0:
                WildfireEvent();
                break;
            case 1:
                OilEvent();
                break;
            case 2:
                VolcanoEvent();
                break;
            case 3:
                TsunamiEvent();
                break;
            case 4:
                SuperGauEvent();
                break;
        }

        for (int i = 0; i < Mathf.Abs(secondsToImpact); i++)
        {
            //Iterate seconds from event start
        }
    }

    public void CatastropheAvoided()
    {
        survived = true;
        ES3.Save("randomEventSurvived", survived);
        Destroy(transform.GetChild(0).gameObject);
    }

    public void WildfireEvent()
    {
        GameObject.FindGameObjectWithTag("Atmosphere").GetComponent<SgtAtmosphereDepthTex>().HorizonColor = new Color32(255, 204, 204, 255);// rot (FFCCCC) (OV = CCFFFB)
    }

    public void OilEvent()
    {
        transform.parent.GetChild(0).GetComponent<SgtPlanetWaterGradient>().Shallow = Color.black;
    }

    public void VolcanoEvent()
    {

    }

    public void TsunamiEvent()
    {
        transform.parent.GetChild(0).GetComponent<SgtPlanetWaterTexture>().Strength = 5;
    }

    public void SuperGauEvent()
    {
        GameObject.FindGameObjectWithTag("Atmosphere").GetComponent<SgtAtmosphere>().Color = new Color32(249, 255, 0, 255); // gelb (F9FF00)
    }
}