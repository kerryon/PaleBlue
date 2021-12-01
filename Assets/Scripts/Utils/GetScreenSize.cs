using UnityEngine;

public class GetScreenSize : MonoBehaviour
{
    public RectTransform parentSize;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(parentSize.sizeDelta.x * transform.childCount, 0);
    }
}
