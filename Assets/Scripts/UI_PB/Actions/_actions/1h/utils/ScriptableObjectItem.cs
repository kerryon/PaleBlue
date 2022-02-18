using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item (Trash)")]
public class ScriptableObjectItem : ScriptableObject
{
    [TextArea(2, 5)]
    public string item;
    [TextArea(5, 20)]
    public string description;
}
