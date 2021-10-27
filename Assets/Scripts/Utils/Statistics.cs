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

    public Image waterIndicator;
    public Image humanIndicator;

    void Start()
    {
        water = ES3.Load("WATER", 0f);
        human = ES3.Load("HUMAN", 0);
    }

    void Update()
    {
        waterIndicator.fillAmount = water / 100;
        humanIndicator.fillAmount = human / 100;
    }
}
