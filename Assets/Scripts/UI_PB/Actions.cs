using TMPro;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public GameObject actionPie;
    public GameObject infoWrapper;

    private readonly int maxHours = 168;

    void Update()
    {
        timeDisplay.text = "Verfügbare Zeit\n<font=Fonts/Config-Bold><size=180%>" + Variables.Instance.actionHours + " h</size></font>\n<color=#609AFF1D>von restlichen " + (maxHours - (int)Variables.Instance.timespan.TotalHours) + " h</color>";
    }

    void OnEnable()
    {
        ShowInitial();
    }

    public void ShowInitial()
    {
        infoWrapper.SetActive(false);
        actionPie.SetActive(true);

        for (int i = 1; i < transform.GetChild(1).childCount; i++)
        {
            Destroy(transform.GetChild(1).transform.GetChild(i).gameObject);
        }
    }

    public void OpenActionInfo(int index)
    {
        infoWrapper.SetActive(true);
    }

    public void CloseActionInfo()
    {
        infoWrapper.SetActive(false);
    }
}
