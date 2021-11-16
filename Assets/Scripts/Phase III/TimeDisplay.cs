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
        time.text = Variables.Instance.timespan.ToString("'<font=Fonts/Config-Bold><size=180%>'d'</size></font> <color=#609AFF1D>d</color> <font=Fonts/Config-Bold><size=180%>'hh'</size></font> <color=#609AFF1D>h</color> <font=Fonts/Config-Bold><size=180%>'mm'</size></font> <color=#609AFF1D>min</color>\n<color=#609AFF80>überlebt</color>'");
    }
}
