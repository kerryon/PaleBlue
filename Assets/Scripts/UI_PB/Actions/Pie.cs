using UnityEngine;

[CreateAssetMenu(fileName = "Pie", menuName = "PieMenu/Pie", order = 1)]
public class Pie : ScriptableObject
{
    public PieElement[] Elements;
}
