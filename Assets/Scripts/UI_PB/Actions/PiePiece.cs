using UnityEngine;
using UnityEngine.UI;

public class PiePiece : MonoBehaviour
{
    public Image Icon;
    public Image CakePiece;
    private Actions actionsScript;

    void Start()
    {
        actionsScript = transform.parent.GetComponentInParent<Actions>();
    }

    public void GetPiePiece()
    {
        if (!string.IsNullOrEmpty(GetComponentInParent<PieMenu>().Data.Elements[transform.GetSiblingIndex()].actionName))
        {
            actionsScript.OpenActionInfo(transform.GetSiblingIndex());
        }

        GetComponentInParent<PieMenu>().SelectPie(transform.GetSiblingIndex());
    }
}
