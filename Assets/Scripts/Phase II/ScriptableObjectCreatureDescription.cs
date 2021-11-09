using UnityEngine;

[CreateAssetMenu(fileName = "New Card Description", menuName = "CardDescription")]
public class ScriptableObjectCreatureDescription : ScriptableObject
{
    [TextArea(10, 30)]
    public string description;
}
