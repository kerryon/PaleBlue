using UnityEngine;

[CreateAssetMenu(fileName = "New PieElement", menuName = "PieMenu/Element", order = 2)]
public class PieElement : ScriptableObject
{
    public int index;
    [TextArea(1, 5)]
    public string pathName;
    [TextArea(2, 5)]
    public string actionName;
    [TextArea(15, 35)]
    public string actionDescription;
    public Sprite Icon;
    public Pie NextPie;

    public string topic;
    [TextArea(15, 20)]
    public string description;

    [TextArea(3, 20)]
    public string FABText;

    [TextArea(2, 20)]
    public string FABInfoTitle;
    [TextArea(15, 20)]
    public string FABInfo;
    public Sprite titleImage;


}