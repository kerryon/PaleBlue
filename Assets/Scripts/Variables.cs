using System;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private static Variables _instance;

    public static Variables Instance { get { return _instance; } }

    [Header("Utillity")]
    public int phaseCount;

    [Header("Parameter")]
    public float water;
    public int human;
    public int currentLevelIndex;

    [Header("Difficulty Variables")]
    public float waterUseRate;
    public float reproductionRate;
    public float waterStorageRate;


    public DateTime currentDate;
    public TimeSpan timespan;
    DateTime Started;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        currentLevelIndex = ES3.Load("CLI", 3);
        water = ES3.Load("WATER", 0f);
        human = ES3.Load("HUMAN", 0);
        waterUseRate = ES3.Load("Property_waterUseRate", 0f);
        reproductionRate = ES3.Load("Property_reproductionRate", 0f);
        waterStorageRate = ES3.Load("Property_storageRate", 0f);
    }

    void Start()
    {
        currentDate = DateTime.Now;

        if (Instance.currentLevelIndex <= 3)
        {
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);
    }

    void Update()
    {
        currentDate = DateTime.Now;
        timespan = currentDate.Subtract(Started);
        //timespan = vergangene Zeit seit Start
    }

    private void OnApplicationFocus(bool focus)
    {

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            ES3.Save("CLI", currentLevelIndex);
            ES3.Save("WATER", water);
            ES3.Save("HUMAN", human);
        }
    }

    void OnApplicationQuit()
    {
        ES3.Save("CLI", currentLevelIndex);
        ES3.Save("WATER", water);
        ES3.Save("HUMAN", human);

        ES3.Save("CurrentTime", timespan);
    }
}