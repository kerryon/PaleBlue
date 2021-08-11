using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    private TimeDetect TimeDetect;

    void Start()
    {
        TimeDetect = GameObject.FindGameObjectWithTag("LL").GetComponent<TimeDetect>();
    }

    void Update()
    {
        gameObject.GetComponent<TMP_Text>().text = TimeDetect.difference.ToString("'Verbrachte Zeit: 'd' Tage,\n 'hh' Stunden, 'mm' Minuten'");
    }
}
