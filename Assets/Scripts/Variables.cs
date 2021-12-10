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
    public int currentActionIndex;

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

        InvokeRepeating(nameof(ValueCalculation), 0, 1.0f);
    }

    public void ValueCalculation()
    {
        float wC = Mathf.InverseLerp(0f, maxWater, water);
        float hC = Mathf.InverseLerp(0f, water, human * waterUseRate);
        float rC = Mathf.InverseLerp(0f, 300f, rain);

        if (human * waterUseRate < water)
        {
            human += Mathf.Pow(human, Mathf.Lerp(0.01f, 0.1f, reproductionRate));
        }
        else if (human * waterUseRate > water && human > 0)
        {
            human -= Mathf.Pow(human, Mathf.Lerp(0.01f, 0.1f, waterStorageRate));
        }
        else
        {
            human = 0f;
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
        
        h_conflict = Map((1 - wC) + hC + w + c, 0f, 4f, 0f, 1f) + ((h_urbanisation + h_agriculture + h_waterStructure) * 0.1f);
        h_luxury = Map((1 - wC) + hC + w + e + c, 0f, 11f, 0f, 1f) + ((h_industry + h_agriculture) * 0.1f);
        h_industry = Map((1 - wC) + hC + s + e, 0f, 17f, 0f, 1f);
        h_agriculture = Map((1 - wC) + hC + s + c, 0f, 10f, 0f, 1f) + ((h_industry + h_urbanisation + h_waterStructure) * 0.1f);
        h_waste = Map(hC + w * 2 + e, 0f, 11f, 0f, 1f) + ((h_industry + h_luxury + h_overfishing) * 0.1f);
        h_urbanisation = Map(hC + e, 0f, 8f, 0f, 1f) + (h_industry * 0.1f);
        h_energy = Map(hC + w + e, 0f, 9f, 0f, 1f) + ((h_urbanisation + h_industry) * 0.1f);
        h_overfishing = Map(hC + e + c, 0f, 9f, 0f, 1f) + ((h_industry + h_luxury) * 0.1f);
        h_wastewater = Map(hC + w + e * 2, 0f, 16f, 0f, 1f) + ((h_industry + h_agriculture) * 0.1f);
        h_waterStructure = Map(s + e, 0f, 14f, 0f, 1f) + ((h_industry + h_energy) * 0.1f);
        //Grundwerte mit '+=' ?
        w_distribution += Map(h_conflict + h_luxury + h_waterStructure, 0f, 3f, 0f, 1f);
        w_current = Map(w_temperature + w_ice, 0f, 2f, 0f, 1f);
        w_contamination = Map(h_waste + h_wastewater, 0f, 2f, 0f ,1f);
        w_temperature = Map(w_carbonDioxide + w_ice, 0f, 2f, 0f, 1f);
        w_weatherExtremes = Map(h_waterStructure + (1 - wC), 0f, 2f, 0f, 1f);
        w_carbonDioxide = Map(h_energy + h_industry, 0f, 2f, 0f, 1f);
        w_fishCount = Map(h_overfishing + h_waste + w_temperature + w_carbonDioxide, 0f, 4f, 0f ,1f);
        w_groundwater = Map(h_urbanisation + h_agriculture + h_waterStructure + w_distribution, 0f, 4f, 0f, 1f);
        w_trees = Map(h_agriculture + h_urbanisation, 0f, 2f, 0f, 1f);
        w_ice = Map(w_carbonDioxide + w_temperature, 0f, 2f, 0f, 1f);
        // Grundwerte beeinflussen?
        waterEcology = Map(w_contamination + w_temperature + w_fishCount + w_carbonDioxide, 0f, 4f, 0f, 1f);
        waterQuality = Map(w_contamination + w_carbonDioxide, 0f, 2f, 0f, 1f);
        waterQuantity = Map(w_groundwater + w_distribution + w_trees + w_weatherExtremes + +(1 - wC), 0f, 5f, 0f, 1f);
        waterSealevel = Map(w_current + w_temperature + w_ice, 0f, 3f, 0f, 1f);

        regenerationRate = Map(rain, 0f, 300f, -1f, 2f); // s + e + c

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

    private float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
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

        // >>> InvokeRepeating Alternative
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

            water = ES3.Load("WATER", maxWater);
            human = ES3.Load("HUMAN", 1000f);
            rain = ES3.Load("RAIN", 300f);
        }
        else
        {
            historyCount = 1;

            water = maxWater;
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