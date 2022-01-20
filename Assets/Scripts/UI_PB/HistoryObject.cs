using UnityEngine;
using TMPro;

public class HistoryObject : MonoBehaviour
{
    public GameObject TextObject;

    private TMP_Text text;
    Menu _menu;

    public void GetText()
    {
        _menu = gameObject.transform.parent.GetComponentInParent<Menu>();
        text = TextObject.GetComponent<TMP_Text>();

        var sheet = new ES3Spreadsheet();
        sheet.Load("history.csv");
        int value = sheet.GetCell<int>(1, transform.GetSiblingIndex() - 2);

        if (value < _menu.historyContent.Length)
        {
            text.text = "<font=Fonts/Config-Bold><size=150%><line-height=50%>" + _menu.historyContent[value].FABInfoTitle.Replace(";", "\n") + "</line-height></size></font>" + "\n\n<line-indent=5%>" + _menu.historyContent[value].FABInfo.Replace(";", "\n");
        }
        else
        {
            text.text = "<font=Fonts/Config-Bold><size=150%><line-height=50%>" + _menu.actionContent[value - _menu.historyContent.Length].FABInfoTitle.Replace(";", "\n") + "</line-height></size></font>" + "\n\n<line-indent=5%>" + _menu.actionContent[value - _menu.historyContent.Length].FABInfo.Replace(";", "\n");
        }
    }
}
