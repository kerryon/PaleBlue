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
        int num = transform.GetSiblingIndex();

        Variables.Instance.currentActionIndex = GetComponentInParent<PieMenu>().Data.Elements[num].index;

        if (!string.IsNullOrEmpty(GetComponentInParent<PieMenu>().Data.Elements[num].actionName))
        {
            actionsScript.OpenActionInfo(num);
        }

        GetComponentInParent<PieMenu>().SelectPie(num);
    }
}
