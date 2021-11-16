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

        text.text = "<font=Fonts/Config-Bold>" + _menu.historyContent[value].FABInfoTitle.Replace(";", "\n") + "</font>" + "\n\n" + _menu.historyContent[value].FABInfo.Replace(";", "\n");
    }
}
