using UnityEngine;

[CreateAssetMenu(fileName = "New Goal", menuName = "Goal")]
public class ScriptableObjectGoals : ScriptableObject
{
    [TextArea(5, 20)]
    public string description;
}
