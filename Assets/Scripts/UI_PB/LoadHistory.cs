using UnityEngine;

public class LoadHistory : MonoBehaviour
{
    public Menu _menu;

    void OnEnable()
    {
        _menu.LoadHistory();
    }
}
