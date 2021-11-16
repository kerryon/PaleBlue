using LylekGames;
using UnityEngine;

public class DrawScriptExtend : MonoBehaviour
{
    public GameObject button; 

    void LateUpdate()
    {
        //if (setBrush)
        //{
        //    DrawScript.drawScript.SetBrushColor(new Color32(133, 33, 15, 80));
        //    DrawScript.drawScript.SetBrushSize(20);
        //    setBrush = false;
        //}

        if (ES3.KeyExists("LIFE") && DrawScript.drawScript.drawHistory.Count > 0)
        {
            button.SetActive(true);
        }
    }
}
