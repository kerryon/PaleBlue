using UnityEngine;
using UnityEngine.Events;

public class PinPrefab : MonoBehaviour
{

    public PopUp popup;
    public UnityEvent[] onClick;
    [Header("Color after selection")]
    public Color selectedColor = new Color32(255, 96, 96, 255); //red
    [Header("Max Pins")]
    public int maxPins;

    Menu _menu;

    void Start()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Menu>();
    }

    public void PopupContentDistribution()
    {
        int num;
        num = System.Convert.ToInt32(gameObject.transform.name);

        if (num < maxPins)
        {
            //Semicolon wird mit einem Absatz ersetzt
            popup.description.text = popup.popUpContent[num].description.Replace(";", "\n");
            popup.FABText.text = popup.popUpContent[num].FABText;
            popup.FABInfoTitle.text = popup.popUpContent[num].FABInfoTitle;
            popup.FABInfo.text = popup.popUpContent[num].FABInfo.Replace(";", "\n");
            popup.titleImage.sprite = popup.popUpContent[num].titleImage;
            popup.button.onClick.RemoveAllListeners();
            if (num != maxPins - 1)
            {
                popup.button.onClick.AddListener(() => onClick[0]?.Invoke());
            } else
            {
                popup.button.onClick.AddListener(() => onClick[1]?.Invoke());
            }
        }

        if (transform.GetChild(0).GetComponent<SpriteRenderer>().color != selectedColor)
        {
            PinSelected(num);
        }
    }

    public void PinSelected(int num)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = selectedColor;
        _menu.AppendHistory(popup.popUpContent[num].index);
    }
}
