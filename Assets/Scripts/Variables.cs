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
    private float maxWater = 10000f;
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
    public float w_current;
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

    void Awake()
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

        InvokeRepeating(nameof(ValueCalculation), 0, 1.0f); //?
    }

    public void ValueCalculation()
    {
        float wC = Mathf.InverseLerp(0f, maxWater, water);
        float hC = Mathf.InverseLerp(0f, water, human);
        float rC = Mathf.InverseLerp(0f, 300f, rain);

        if (human * waterUseRate < water)
        {
            human += Mathf.Pow(human, Mathf.Lerp(0.01f, 0.1f, reproductionRate));
        }
        else
        {
            human -= Mathf.Pow(human, Mathf.Lerp(0.01f, 0.1f, waterStorageRate));
        }

        if (rain > 0f)
        {
            rain -= 300f / 18000f;
        }
        else
        {
            rain = 0f;
        }

        s = (int)timespan.TotalDays;
        e = s * wC;
        w = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, 2f, c + wC));
        c = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(0f, 2f, w_current + w_contamination));
        
        h_conflict = wC + hC + w + c    + (h_urbanisation + h_agriculture + h_waterStructure);
        h_luxury = wC + hC + w + e + c  + (h_industry + h_agriculture);
        h_industry = wC + hC + s + e;
        h_agriculture = wC + hC + s + c + (h_industry + h_urbanisation + h_waterStructure);
        h_waste = hC + w * 2 + e        + (h_industry + h_luxury + h_overfishing);
        h_urbanisation = hC + e         + (h_industry);
        h_energy = hC + w + e           + (h_urbanisation + h_industry);
        h_overfishing = hC + e + c      + (h_industry + h_luxury);
        h_wastewater = hC + w + e * 2   + (h_industry + h_agriculture);
        h_waterStructure = s + e        + (h_industry + h_energy);

        w_distribution = h_conflict + h_luxury + h_waterStructure;
        w_current = w_temperature + w_ice;
        w_contamination = h_waste + h_wastewater;
        w_temperature = w_carbonDioxide + w_ice;
        w_weatherExtremes = h_waterStructure + rC;
        w_carbonDioxide = h_energy + h_industry;
        w_fishCount = h_overfishing + h_waste + w_temperature + w_carbonDioxide;
        w_groundwater = h_urbanisation + h_agriculture + h_waterStructure + w_distribution;
        w_trees = h_agriculture;
        w_ice = w_carbonDioxide;

        waterEcology = w_contamination + w_temperature + w_fishCount + w_carbonDioxide;
        waterQuantity = w_contamination + w_carbonDioxide;
        waterQuality = w_groundwater + w_distribution + w_trees + w_weatherExtremes;
        waterSealevel = w_current + w_temperature + w_ice;

        regenerationRate = Mathf.Lerp(-1f, 2f, Mathf.InverseLerp(0f, 300f, rain)); // s + e + c

        if (water >= 0f && water <= maxWater)
        {
            water += regenerationRate;
        }
        else if (water > maxWater)
        {
            water = maxWater;
        }
        else if (water < 0f)
        {
            water = 0f;
        }
        //Debug.Log(" | " + " | ");
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
        //    ValueCalculation();
        //}
    }

    public void AddHours(int hours)
    {
        actionHours += hours;
        ES3.Save("Property_actionHours", actionHours);
    }

    private void UpdateValues()
    {
        lastClosed = ES3.Load("LastClosedAt", lastClosed);
        for (int i = 0; i < (int)DateTime.Now.Subtract(lastClosed).TotalSeconds; i++)
        {
            ValueCalculation();
        }
    }
#if UNITY_IOS || UNITY_EDITOR
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            UpdateValues();
        }
        else
        {
            SaveAllValues();
        }
    }
#endif

#if UNITY_ANDROID
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllValues();
        }
        else
        {
            UpdateValues();
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