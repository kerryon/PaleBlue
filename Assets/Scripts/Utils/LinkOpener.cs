using UnityEngine;
using TMPro;
 
[RequireComponent(typeof(TMP_Text))]
public class LinkOpener : MonoBehaviour
{
    public Camera UICam;
    private Vector3 fingerPosition;

    void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            TMP_Text pTextMeshPro = GetComponent<TMP_Text>();

            Vector2 pos = touch.position;
            fingerPosition = new Vector3(pos.x, pos.y, 0f);
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, fingerPosition, UICam);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            TMP_Text pTextMeshPro = GetComponent<TMP_Text>();

            Vector2 pos = Input.mousePosition;
            fingerPosition = new Vector3(pos.x, pos.y, 0f);
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, fingerPosition, UICam);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }
#endif
    }
}