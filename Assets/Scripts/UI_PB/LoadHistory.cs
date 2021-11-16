using UnityEngine;

public class LoadHistory : MonoBehaviour
{
    public Menu menu;

    void OnEnable()
    {
        menu.LoadHistory();
    }
}
