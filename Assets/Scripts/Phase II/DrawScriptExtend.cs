using LylekGames;
using UnityEngine;

public class DrawScriptExtend : MonoBehaviour
{
    public GameObject button;

    void Start()
    {
        ES3.DeleteKey("LIFE");
    }

    void LateUpdate()
    {
        if (ES3.KeyExists("LIFE") && DrawScript.drawScript.drawHistory.Count > 0)
        {
            button.SetActive(true);
        }
    }
}
