using System;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private static Variables _instance;

    public static Variables Instance { get { return _instance; } }

    [Header("Utillity")]
    public int historyCount;
    public int actionHours;
    public int actionCount;

    [Header("Parameter")]
    public float water;
    public float human;
    public int currentLevelIndex;

    [Header("Difficulty Variables")]
    public float waterUseRate;
    public float reproductionRate;
    public float waterStorageRate;

    [Header("Water Rates")]
    public float regenerationRate;
    public float rain;

    [Header("Human Values")]
    public int s; //Stadium
    public float e; //Wirtschaft
    public float w; //Wohlergehen
    public float c; //Klima

    [Header("Water Values")]
    public float waterEcology;
    public float waterQuality;
    public float waterQuantity;
    public float waterSealevel;

    [Header("HumanActionAreas")]
    public float h_conflict;
    public float h_luxury;
    public float h_industry;
    public float h_agriculture;
    public float h_waste;
    public float h_urbanisation;
    public float h_energy;
    public float h_overfishing;
    public float h_wastewater;
    public float h_waterStructure;


    [Header("WaterActionAreas")]
    public float w_distribution;
    public float w_oceanCurrent;
    public float w_contamination;
    public float w_temperature;
    public float w_weatherExtremes;
    public float w_carbonDioxide;
    public float w_fishCount;
    public float w_groundwater;
    public float w_trees;
    public float w_ice;


    public DateTime currentDate;
    public TimeSpan timespan;
    public DateTime lastClosed;
    DateTime Started;

    //private float timer;

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

        LoadAllValues();
    }

    void Start()
    {

        if (currentLevelIndex <= 3)
        {
            currentDate = DateTime.Now;
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);

        InvokeRepeating(nameof(InitialValueCalculation), 0, 1.0f);
    }

    private void InitialValueCalculation()
    {
        human += reproductionRate;
        rain  -= 300f/18000f;

        s = (int)timespan.TotalDays;
        e = s * w * Mathf.Lerp(0, 2, Mathf.InverseLerp(0, 10000, water));
        w = e * c * Mathf.Lerp(0, 2, Mathf.InverseLerp(0, 10000, water));
        c = w_oceanCurrent + w_contamination;

        h_conflict       = water + human + w + c;
        h_luxury         = water + human + w + e + c;
        h_industry       = water + human + s + e;
        h_agriculture    = water + human + s + c;
        h_waste          = human + w*2 + e;
        h_urbanisation   = human + e;
        h_energy         = human + w + e;
        h_overfishing    = human + e + c;
        h_wastewater     = human + w + e*2;
        h_waterStructure = s + e;

        w_distribution    = 0;
        w_oceanCurrent    = 0;
        w_contamination   = 0;
        w_temperature     = 0;
        w_weatherExtremes = 0;
        w_carbonDioxide   = 0;
        w_fishCount       = 0;
        w_groundwater     = 0;
        w_trees           = 0;
        w_ice             = 0;

        waterEcology  = 0;
        waterQuantity = 0;
        waterQuality  = 0;
        waterSealevel = 0;

        regenerationRate = (int)human * waterUseRate * (s + e + c) * Mathf.Lerp(0, 300, rain);
        water            += regenerationRate;

        Debug.Log("human = " + (int)human + " // rain = " + rain + " // regenerationsrate = " + regenerationRate + " // " + e + " // " + w + " // " + c); //DEBUG CHECK
    }

    void Update()
    {
        //timespan = vergangene Zeit seit Spielstart
        currentDate = DateTime.Now;
        timespan = currentDate.Subtract(Started);

        //calculate usable hours
        if ((int)timespan.TotalHours > actionCount)
        {
            actionCount++;
            AddHours(1);
        }

        // ANSTELLE VON InvokeRepeating ???
        //timer -= Time.deltaTime;
        //if (timer < 0)
        //{
        //    timer = 1f;
        //    InitialValueCalculation();
        //}
    }

    public void AddHours(int hours)
    {
        actionHours += hours;
        ES3.Save("Property_actionHours", actionHours);
    }

    private void AddValuesAfterExit()
    {
        lastClosed = ES3.Load("LastClosedAt", lastClosed);
        for (int i = 0; i < (int)DateTime.Now.Subtract(lastClosed).TotalSeconds; i++)
        {
            InitialValueCalculation();
        }
    }
#if UNITY_IOS || UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AddValuesAfterExit();
        }
        else
        {
            SaveAllValues();
        }
    }
#endif

#if UNITY_ANDROID || UNITY_EDITOR
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllValues();
        }
        else
        {
            AddValuesAfterExit();
        }
    }
#endif

    void OnApplicationQuit()
    {
        SaveAllValues();
    }

    private void LoadAllValues()
    {
        currentLevelIndex = ES3.Load("CLI", 3);
        if (currentLevelIndex > 3)
        {
            historyCount = ES3.Load("HC", 1);

            water = ES3.Load("WATER", 10000f);
            human = ES3.Load("HUMAN", 1000f);
            rain = ES3.Load("RAIN", 300f);
        }
        else
        {
            historyCount = 1;

            water = 10000f;
            human = 1000f;
            rain = 300f;
        }
        waterUseRate = ES3.Load("Property_waterUseRate", 0.2f);
        reproductionRate = ES3.Load("Property_reproductionRate", 0.2f);
        waterStorageRate = ES3.Load("Property_storageRate", 0.2f);
        actionHours = ES3.Load("Property_actionHours", 0);
        actionCount = ES3.Load("Property_actionCounter", 0);
    }

    private void SaveAllValues()
    {
        ES3.Save("CLI", currentLevelIndex);
        ES3.Save("HC", historyCount);
        ES3.Save("WATER", water);
        ES3.Save("HUMAN", human);
        ES3.Save("RAIN", rain);
        ES3.Save("Property_waterUseRate", waterUseRate);
        ES3.Save("Property_reproductionRate", reproductionRate);
        ES3.Save("Property_storageRate", waterStorageRate);
        ES3.Save("Property_actionCounter", actionCount);
        ES3.Save("LastClosedAt", DateTime.Now);
    }
}