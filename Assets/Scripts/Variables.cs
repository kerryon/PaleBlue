using System;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private static Variables _instance;

    public static Variables Instance { get { return _instance; } }

    [Header("Utillity")]
    public int historyCount;

    [Header("Parameter")]
    public float water;
    public int human;
    public int currentLevelIndex;

    [Header("Difficulty Variables")]
    public float waterUseRate;
    public float reproductionRate;
    public float waterStorageRate;

    [Header("Human Values")]
    public int humanState;
    public float humanWellbeing;
    public float humanEconomy;
    public float humanClimate;

    [Header("Water Values")]
    public float waterEcology;
    public float waterQuality;
    public float waterQuantity;
    public float waterSealevel;


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
        if (currentLevelIndex > 3)
        {
            historyCount = ES3.Load("HC", 1);
        } else
        {
            historyCount = 1;
        }
        water = ES3.Load("WATER", 0f);
        human = ES3.Load("HUMAN", 0);
        waterUseRate = ES3.Load("Property_waterUseRate", 0f);
        reproductionRate = ES3.Load("Property_reproductionRate", 0f);
        waterStorageRate = ES3.Load("Property_storageRate", 0f);
    }

    void Start()
    {
        currentDate = DateTime.Now;

        if (currentLevelIndex <= 3)
        {
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);
    }

    void Update()
    {
        currentDate = DateTime.Now;
        //timespan = vergangene Zeit seit Start
        timespan = currentDate.Subtract(Started);
    }

    private void OnApplicationFocus(bool focus)
    {

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            ES3.Save("CLI", currentLevelIndex);
            ES3.Save("HC", historyCount);
            ES3.Save("WATER", water);
            ES3.Save("HUMAN", human);
            ES3.Save("Property_waterUseRate", waterUseRate);
            ES3.Save("Property_reproductionRate", reproductionRate);
            ES3.Save("Property_storageRate", waterStorageRate);
        }
    }

    void OnApplicationQuit()
    {
        ES3.Save("CLI", currentLevelIndex);
        ES3.Save("HC", historyCount);
        ES3.Save("WATER", water);
        ES3.Save("HUMAN", human);
        ES3.Save("Property_waterUseRate", waterUseRate);
        ES3.Save("Property_reproductionRate", reproductionRate);
        ES3.Save("Property_storageRate", waterStorageRate);
    }
}