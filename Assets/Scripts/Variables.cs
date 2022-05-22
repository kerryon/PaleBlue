using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private static Variables _instance;

    public static Variables Instance { get { return _instance; } }

    [Header("Utillity")]
    public int historyCount; // lowest = 1
    public int actionHours;
    public int actionCount;
    public int currentActionIndex;
    [HideInInspector] public float[] values = new float[20];
    [HideInInspector] public bool valuesSet = false;

    [Header("Parameter")]
    public float water;
    public float human;
    public float deaths;
    public int currentLevelIndex;

    [Header("Difficulty Variables")]
    public float waterUseRate;
    public float reproductionRate;
    public float waterStorageRate;

    [Header("Water Rates")]
    public float regenerationRate;
    public float rain;

    [Header("Human Values")]
    public float s; // Stadium
    public float e; // Wirtschaft
    public float w; // Wohlergehen
    public float c; // Klima

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
    public float h_wasteWater;
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
    public DateTime Started;

    private bool gameOver = false;
    private readonly float wv = 100000f;
    private readonly float hv = 50000f;
    private readonly float maxRain = 300f;
    private readonly float state = 7f / 604800f;

    [Header("WaterMaxValue")]
    public float maxWater = 10000f; //muss von Spielenden erhöht werden

    private float wC;
    private float hC;
    private float rC;

    List<float> ll = new List<float>();

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
        if (currentLevelIndex <= 4)
        {
            currentDate = DateTime.Now;
            ES3.Save("StartedAt", currentDate);
        }
        Started = ES3.Load("StartedAt", currentDate);

        if (currentLevelIndex != 8)
        {
            InvokeRepeating(nameof(ValueCalculation), 0, 1.0f);

            if (currentLevelIndex == 7)
            {
                UpdateValues();
            }

            StartCoroutine(TrackLife());
        }
    }

    private IEnumerator TrackLife()
    {
        ll = ES3.Load("LifeLine", ll);

        yield return new WaitForSeconds(0.2f);

        ll.Add(human);
        ES3.Save("LifeLine", ll);
    }

    public void SetValues()
    {
        if (ES3.KeyExists("VALUES") && currentLevelIndex > 6)
        {
            values = ES3.Load("VALUES", values);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                values[i] = 20000f;
                values[i + 10] = 80000f;
            }
            rain = maxRain;
            human = 1000f;
            water = maxWater;
        }

        h_conflict = values[0];
        h_luxury = values[1];
        h_industry = values[2];
        h_agriculture = values[3];
        h_waste = values[4];
        h_urbanisation = values[5];
        h_energy = values[6];
        h_overfishing = values[7];
        h_wasteWater = values[8];
        h_waterStructure = values[9];

        w_distribution = values[10];
        w_current = values[11];
        w_contamination = values[12];
        w_temperature = values[13];
        w_weatherExtremes = values[14];
        w_carbonDioxide = values[15];
        w_fishCount = values[16];
        w_groundwater = values[17];
        w_trees = values[18];
        w_ice = values[19];
    }

    public void ValueCalculation()
    {
        if (!valuesSet)
        {
            valuesSet = true;
            SetValues();
        }

        if (timespan.TotalDays >= 7 && !gameOver)
        {
            gameOver = true;
            GameOver();
        }

        wC = Mathf.InverseLerp(0f, maxWater, water);
        hC = Mathf.InverseLerp(0f, water, human * waterUseRate);
        rC = Mathf.Abs(Mathf.LerpUnclamped(1f , 0f, rain / maxRain));

        if (human * waterUseRate < water && human > 0)
        {
            //human += Mathf.Pow(human, Mathf.Lerp(0.001f, 0.007f, reproductionRate));
            float rate = reproductionRate * (1 + (1 - hC)) + Map(human, 0f, 604800f, 0f, 3f);
            human += rate;
        }
        else if (human * waterUseRate > water && human > 0)
        {
            //float rate = Mathf.Pow(human, Mathf.Lerp(0.001f, 0.007f, waterStorageRate));
            float rate = waterStorageRate * (1 + Mathf.InverseLerp(human * waterUseRate, 0f, water)) + Map(human, 0f, 604800f, 0f, 3f);
            human -= rate;
            deaths += rate;
        }
        else
        {
            human = 0f;
            if (!gameOver) {
                gameOver = true;
                GameOver();
            }
        }

        if (rain > 0f)
        {
            rain -= maxRain / 25200f; // von 300 auf 0 in 7 Stunden
        }
        else
        {
            rain = 0f;
        }

        //s = (float)timespan.TotalDays;
        s += state;
        e = s * wC;
        w = Mathf.InverseLerp(0f, 10f, c * 0.5f + (1 - wC) * 0.5f + hC * 3f + Map(waterQuality + waterQuantity + waterEcology + waterSealevel, 0f, 4f, 0f, 6f));
        c = Mathf.InverseLerp(wv*3, 0f, w_current + w_carbonDioxide + w_trees);
        
        h_conflict += Map((1 - wC) + hC + w + c, 0f, 4f, 0f, 1f) + Mathf.InverseLerp(0f, hv*3, h_urbanisation + h_agriculture + h_waterStructure);
        h_luxury += Map((1 - wC) + hC + w + e + c, 0f, 11f, 0f, 1f) + Mathf.InverseLerp(0f, hv*2, h_industry + h_agriculture);
        h_industry += Map((1 - wC) + hC + s + e, 0f, 17f, 0f, 1f);
        h_agriculture += Map((1 - wC) + hC + s + c, 0f, 10f, 0f, 1f) + Mathf.InverseLerp(0f, hv*3, h_industry + h_urbanisation + h_waterStructure);
        h_waste += Map(hC + w * 2 + e, 0f, 11f, 0f, 1f) + Mathf.InverseLerp(0f, hv*3, h_industry + h_luxury + h_overfishing);
        h_urbanisation += Map(hC + e, 0f, 8f, 0f, 1f) + Mathf.InverseLerp(0f, hv, h_industry);
        h_energy += Map(hC + w + e, 0f, 9f, 0f, 1f) + Mathf.InverseLerp(0f, hv*2, h_urbanisation + h_industry);
        h_overfishing += Map(hC + e + c, 0f, 9f, 0f, 1f) + Mathf.InverseLerp(0f, hv*2, h_industry + h_luxury);
        h_wasteWater += Map(hC + w + e * 2, 0f, 16f, 0f, 1f) + Mathf.InverseLerp(0f, hv*2, h_industry + h_agriculture);
        h_waterStructure += Map(s + e, 0f, 14f, 0f, 1f) + Mathf.InverseLerp(0f, hv*2, h_industry + h_energy);

        if (h_conflict >= hv) { h_conflict = hv; } else if (h_conflict <= 0) { h_conflict = 0; }
        if (h_luxury >= hv) { h_luxury = hv; } else if (h_luxury <= 0) { h_luxury = 0; }
        if (h_industry >= hv) { h_industry = hv; } else if (h_industry <= 0) { h_industry = 0; }
        if (h_agriculture >= hv) { h_agriculture = hv; } else if (h_agriculture <= 0) { h_agriculture = 0; }
        if (h_waste >= hv) { h_waste = hv; } else if (h_waste <= 0) { h_waste = 0; }
        if (h_urbanisation >= hv) { h_urbanisation = hv; } else if (h_urbanisation <= 0) { h_urbanisation = 0; }
        if (h_energy >= hv) { h_energy = hv; } else if (h_energy <= 0) { h_energy = 0; }
        if (h_overfishing >= hv) { h_overfishing = hv; } else if (h_overfishing <= 0) { h_overfishing = 0; }
        if (h_wasteWater >= hv) { h_wasteWater = hv; } else if (h_wasteWater <= 0) { h_wasteWater = 0; }
        if (h_waterStructure >= hv) { h_waterStructure = hv; } else if (h_waterStructure <= 0) { h_waterStructure = 0; }

        w_distribution += Map(h_conflict + h_luxury + h_waterStructure, 0f, hv*3, 1f, -0.5f);
        w_current += Map(w_temperature*2 + w_ice + w_weatherExtremes ,wv*4, 0f, 3f, -2.5f); // !!!
        w_contamination += Map(h_waste + h_wasteWater, 0f, hv*2, 1f, -0.5f);
        w_temperature += Map(w_carbonDioxide*2 + w_ice, wv*3, 0f, 3f, -2.5f); // !!!
        w_weatherExtremes += Map(Mathf.InverseLerp(0f, hv, h_waterStructure) + (1 - wC) + rC, 0f, 3f, 1f, -0.5f);
        w_carbonDioxide += Map(h_energy + h_industry, 0f, hv*2, 1f, -0.5f);
        w_fishCount += Map(Mathf.InverseLerp(0f ,hv, h_overfishing)*2 + Mathf.InverseLerp(0f, hv, h_waste) + Mathf.InverseLerp(wv, 0f, w_temperature) + Mathf.InverseLerp(wv, 0f, w_carbonDioxide), 0f, 5f, 2f, -1.5f); // !!!
        w_groundwater += Map(Mathf.InverseLerp(0f, hv, h_urbanisation) + Mathf.InverseLerp(0f, hv, h_agriculture) + Mathf.InverseLerp(0f, hv, h_waterStructure) + Mathf.InverseLerp(wv, 0f, w_distribution) + rC, 0f, 5f, 1f, -0.5f);
        w_trees += Map(h_agriculture + h_urbanisation, 0f, hv*2, 1f, -0.5f);
        w_ice += Map(w_carbonDioxide*2 + w_temperature, wv*3, 0f, 3f, -2.5f); // !!!

        if (w_distribution >= wv) { w_distribution = wv; } else if (w_distribution <= 0) { w_distribution = 0; }
        if (w_current >= wv) { w_current = wv; } else if (w_current <= 0) { w_current = 0; }
        if (w_contamination >= wv) { w_contamination = wv; } else if (w_contamination <= 0) { w_contamination = 0; }
        if (w_temperature >= wv) { w_temperature = wv; } else if (w_temperature <= 0) { w_temperature = 0; }
        if (w_weatherExtremes >= wv) { w_weatherExtremes = wv; } else if (w_weatherExtremes <= 0) { w_weatherExtremes = 0; }
        if (w_carbonDioxide >= wv) { w_carbonDioxide = wv; } else if (w_carbonDioxide <= 0) { w_carbonDioxide = 0; }
        if (w_fishCount >= wv) { w_fishCount = wv; } else if (w_fishCount <= 0) { w_fishCount = 0; }
        if (w_groundwater >= wv) { w_groundwater = wv; } else if (w_groundwater <= 0) { w_groundwater = 0; }
        if (w_trees >= wv) { w_trees = wv; } else if (w_trees <= 0) { w_trees = 0; }
        if (w_ice >= wv) { w_ice = wv; } else if (w_ice <= 0) { w_ice = 0; }
        
        waterEcology = Map(w_contamination + w_temperature + w_fishCount + w_carbonDioxide, 0f, wv*4, 0f, 1f);
        waterQuality = Map(w_contamination + w_carbonDioxide, 0f, wv*2, 0f, 1f);
        waterQuantity = Map(w_groundwater + w_distribution + w_trees + w_weatherExtremes + water*10, 0f, wv*5, 0f, 1f);
        waterSealevel = Map(w_current + w_temperature + w_ice, 0f, wv*3, 0f, 1f);

        regenerationRate = Map(rain, -50f, maxRain, -1f, 1.5f) + Map(waterQuality + waterQuantity + waterEcology, 0f, 3f, -0.5f, 0.5f);
        water += regenerationRate;
        
        if (water >= maxWater)
        {
            water = maxWater;
        }
        else if (water <= 0f)
        {
            water = 0f;
        }
    }

    private static float Map(float input, float oldLow, float oldHigh, float newLow, float newHigh)
    {
        float t = Mathf.InverseLerp(oldLow, oldHigh, input);
        return Mathf.Lerp(newLow, newHigh, t);
    }

    void Update()
    {
        // timespan = vergangene Zeit seit Spielstart
        currentDate = DateTime.Now;
        timespan = currentDate.Subtract(Started);

        // calculate usable hours
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
        lastClosed = ES3.Load("LastClosedAt", currentDate);
        int sec = (int)DateTime.Now.Subtract(lastClosed).TotalSeconds;
        for (int i = 0; i < sec; i++)
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
        currentLevelIndex = ES3.Load("CLI", 4);
        if (currentLevelIndex > 4)
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
            rain = maxRain;
        }
        waterUseRate = ES3.Load("Property_waterUseRate", 0.4f);
        reproductionRate = ES3.Load("Property_reproductionRate", 0.4f);
        waterStorageRate = ES3.Load("Property_storageRate", 0.4f);
        actionHours = ES3.Load("Property_actionHours", 0);
        actionCount = ES3.Load("Property_actionCounter", 1);
        maxWater = ES3.Load("Property_maxWater", maxWater);
        deaths = ES3.Load("Property_deathCount", deaths);
        s = ES3.Load("Property_stateCount", 0f);

        gameOver = ES3.Load("GameOver", gameOver);
    }

    private void SaveAllValues()
    {
        values = new float[] { h_conflict, h_luxury, h_industry, h_agriculture, h_waste, h_urbanisation, h_energy, h_overfishing, h_wasteWater, h_waterStructure, w_distribution, w_current, w_contamination, w_temperature, w_weatherExtremes, w_carbonDioxide, w_fishCount, w_groundwater, w_trees, w_ice };

        ES3.Save("CLI", currentLevelIndex);
        ES3.Save("HC", historyCount);
        ES3.Save("VALUES", values);
        ES3.Save("WATER", water);
        ES3.Save("HUMAN", human);
        ES3.Save("RAIN", rain);
        ES3.Save("Property_maxWater", maxWater);
        ES3.Save("Property_waterUseRate", waterUseRate);
        ES3.Save("Property_reproductionRate", reproductionRate);
        ES3.Save("Property_storageRate", waterStorageRate);
        ES3.Save("Property_actionCounter", actionCount);
        ES3.Save("Property_actionHours", actionHours);
        ES3.Save("Property_deathCount", deaths);
        ES3.Save("LastClosedAt", DateTime.Now);
        ES3.Save("Property_stateCount", s);

        ES3.Save("GameOver", gameOver);
    }

    public void GameOver()
    {
        CancelInvoke();
        ES3.Save("GameOver", true);
        GameObject.FindGameObjectWithTag("LL").GetComponent<LevelLoader>().LoadNextLvl();
    }
}