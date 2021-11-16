using UnityEngine;

[CreateAssetMenu(fileName = "New Content", menuName = "Content")]
public class ScriptableObjectContent : ScriptableObject
{
    public int index;
    [TextArea(1, 5)]
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