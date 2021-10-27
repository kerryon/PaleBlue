using UnityEngine;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    private TMP_Text time;

    void Start()
    {
        time = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        time.text = Variables.Instance.timespan.ToString("'Verbrachte Zeit: 'd' Tage,\n 'hh' Stunden, 'mm' Minuten'");
    }
}
