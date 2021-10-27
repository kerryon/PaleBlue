using System;
using UnityEngine;

public class TimeDetect : MonoBehaviour
{
    DateTime currentDate;
    public TimeSpan timespan;

    DateTime Started;

    void Start()
    {
        currentDate = DateTime.Now;

        if (Variables.Instance.currentLevelIndex <= 3)
        {
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);
    }

    void Update()
    {
        currentDate = DateTime.Now;
        timespan = currentDate.Subtract(Started);
        //difference = vergangene Zeit
    }

    private void OnApplicationFocus(bool focus)
    {

    }

    void OnApplicationQuit()
    {
        ES3.Save("CurrentAt", timespan);
    }
}
