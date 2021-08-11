using LylekGames;
using UnityEngine;

public class DrawBrushColor : MonoBehaviour
{
    private bool setBrush = true;

    void LateUpdate()
    {
        if (setBrush)
        {
            DrawScript.drawScript.SetBrushColor(new Color32(133, 33, 15, 80));
            DrawScript.drawScript.SetBrushSize(20);
            setBrush = false;
        }
    }
}
