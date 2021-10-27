using UnityEngine;
using TMPro;

public class HistoryObject : MonoBehaviour
{
    [TextArea(15, 20)]
    public string InfoText;
    public GameObject TextObject;
    private TMP_Text text;

    public void AddText()
    {
        text = TextObject.GetComponent<TMP_Text>();
        text.text = InfoText;

        Menu _menu = FindObjectOfType<Menu>();

        _menu.Textbox.gameObject.SetActive(true);
    }
}
