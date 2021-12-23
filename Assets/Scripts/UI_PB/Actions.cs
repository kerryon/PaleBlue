using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public GameObject actionPie;

    public GameObject infoWrapper;
    public TMP_Text details;
    public TMP_Text infoHead;
    public TMP_Text infoCopy;
    public Image infoImage;

    private readonly int maxHours = 168;

    void Update()
    {
        timeDisplay.text = "verfügbare Zeit\n<font=Fonts/Config-Bold><size=200%>" + Variables.Instance.actionHours + " h</size></font>\n<color=#609AFF1D>von restlichen " + (maxHours - (int)Variables.Instance.timespan.TotalHours) + " h</color>";
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
        PieMenu pm = transform.GetChild(1).GetChild(transform.GetChild(1).childCount - 1).GetComponent<PieMenu>();
        infoWrapper.SetActive(true);
        details.text = pm.Data.Elements[index].actionName + "\n<font=Fonts/Config-Text><size=60%>" + pm.Data.Elements[index].FABText + "</size></font>";
        infoHead.text = pm.Data.Elements[index].actionName;
        infoCopy.text = pm.Data.Elements[index].actionDescription;
        infoImage.sprite = pm.Data.Elements[index].titleImage;
    }

    public void CloseActionInfo()
    {
        infoWrapper.SetActive(false);
    }
}
