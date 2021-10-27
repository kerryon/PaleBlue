using UnityEngine;

public class PinPrefab : MonoBehaviour
{

    public PopUp popup;

    public void PopupContentDistribution()
    {
        int num;
        num = System.Convert.ToInt32(gameObject.transform.name);

        if (num < 6)
        {
            //Semicolon wird mit einem Absatz ersetzt
            popup.description.text = popup.popUpContent[num].description.Replace(";", "\n");
            popup.FABText.text = popup.popUpContent[num].FABText;
            popup.FABInfoTitle.text = popup.popUpContent[num].FABInfoTitle;
            popup.FABInfo.text = popup.popUpContent[num].FABInfo.Replace(";", "\n");
            popup.titleImage.sprite = popup.popUpContent[num].titleImage;
        }
    }
}
