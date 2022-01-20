using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject[] events;

    private bool survived;
    private float randomFloat;
    private float totalSeconds;
    private float secondsToImpact;

    void Start()
    {
        if (!ES3.KeyExists("randomEventTimer"))
        {
            randomFloat = Random.Range(0f, 604800f);
            ES3.Save("randomEventTimer", randomFloat);
        }
        totalSeconds = ES3.Load("randomEventTimer", 300000f);
        secondsToImpact = totalSeconds - (float)Variables.Instance.timespan.TotalSeconds;

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
        int catastrophe = (int)CurveWeightedRandom(curve);
        GameObject e = Instantiate(events[catastrophe], gameObject.transform);
        e.name = "randomEvent_" + catastrophe;

        for (int i = 0; i < Mathf.Abs(secondsToImpact); i++)
        {

        }
    }

    private void CatastropheAvoided(GameObject e)
    {
        survived = true;
        ES3.Save("randomEventSurvived", survived);
        Destroy(e);
    }
}