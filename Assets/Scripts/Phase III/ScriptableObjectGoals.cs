using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Goal")]
public class ScriptableObjectGoals : ScriptableObject
{
    [TextArea(2, 5)]
    public string title;
    [TextArea(5, 20)]
    public string description;
}
