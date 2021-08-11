using System;
using UnityEngine;

public class TimeDetect : MonoBehaviour
{
    DateTime currentDate;
    DateTime oldDate;
    public TimeSpan difference;

    DateTime Started;

    void Start()
    {
        currentDate = DateTime.Now;
        int currentLevelIndex = ES3.Load("CLI", 3);

        if (currentLevelIndex <= 3)
        {
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);

        //---------//


        //long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));

        //DateTime oldDate = DateTime.FromBinary(temp);
        //print("oldDate: " + oldDate);

        //---------//

        //difference = currentDate.Subtract(oldDate);
        //print("Difference: " + difference);
    }

    void Update()
    {
        currentDate = DateTime.Now;
        difference = currentDate.Subtract(Started);
        //print("Difference: " + difference);
    }

    private void OnApplicationFocus(bool focus)
    {

    }

    void OnApplicationQuit()
    {
        ES3.Save("CurrentAt", difference);
        //PlayerPrefs.SetString("sysString", DateTime.Now.ToBinary().ToString());
    }
}
