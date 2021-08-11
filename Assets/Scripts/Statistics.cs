using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [Header("Parameter")]
    [Range(0, 100)]
    public float water;
    [Range(0, 100)]
    public float human;
    [Range(-10, 10)]
    public float climate;


    public Image waterIndicator;
    public Image humanIndicator;
    public Image climateIndicator;

    void Start()
    {
        water = ES3.Load("WATER", 0);
        human = ES3.Load("WATER", 0);
        climate = ES3.Load("TEMP", 0);
    }

    void Update()
    {
        waterIndicator.fillAmount = water / 100;
        humanIndicator.fillAmount = human / 100;
    }
}
