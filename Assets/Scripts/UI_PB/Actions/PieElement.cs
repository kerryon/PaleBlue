using UnityEngine;

[CreateAssetMenu(fileName = "PieElement", menuName = "PieMenu/Element", order = 2)]
public class PieElement : ScriptableObject
{
    public string Name;
    [TextArea(2, 5)]
    public string actionName;
    [TextArea(15, 35)]
    public string actionDescription;
    public Sprite Icon;
    public Pie NextPie;
}