using UnityEngine;

public class ShowButtonDelay : MonoBehaviour
{
    public GameObject button;
    public float Delay = 5f;

    void Start()
    {
        button.SetActive(false);

        Invoke(nameof(DisplayButton), Delay);
    }

    void DisplayButton()
    {
        button.SetActive(true);
    }
}
