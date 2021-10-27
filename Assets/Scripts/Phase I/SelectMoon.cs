using UnityEngine;

public class SelectMoon : MonoBehaviour
{
    private GameObject UI;
    private Transform moon;
    private bool isShown = false;

    void Start()
    {
        moon = gameObject.transform.GetChild(0);
        moon.gameObject.SetActive(false);

        UI = transform.parent.GetChild(0).gameObject;
    }

    public void MoonSelect(int number)
    {
        isShown = !isShown;
        moon.gameObject.SetActive(isShown);

        UI.GetComponent<UI>().PhaseFourTrigger(number);
    }

    public void MoonDeselect(int number)
    {
        isShown = !isShown;
        moon.gameObject.SetActive(isShown);

        UI.GetComponent<UI>().PhaseFourDeselectTrigger(number);
    }
}
