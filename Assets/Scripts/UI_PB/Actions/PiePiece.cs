using UnityEngine;
using UnityEngine.UI;

public class PiePiece : MonoBehaviour
{
    public Image Icon;
    public Image CakePiece;

    public void GetPiePiece()
    {
        gameObject.GetComponentInParent<PieMenu>().SelectPie(transform.GetSiblingIndex());
    }
}
