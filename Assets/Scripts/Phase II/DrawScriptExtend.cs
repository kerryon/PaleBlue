using LylekGames;
using UnityEngine;

public class DrawScriptExtend : MonoBehaviour
{
    public GameObject button;
    private Transform history;

    void Start()
    {
        ES3.DeleteKey("LIFE");
        history = DrawScript.drawScript.historyHolder.transform;
    }

    void LateUpdate()
    {
        if (ES3.KeyExists("LIFE") && history.childCount > 0)
        {
            button.SetActive(true);
        }
    }
}
