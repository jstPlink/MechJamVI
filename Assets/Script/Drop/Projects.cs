using UnityEngine;

[CreateAssetMenu(fileName = "Projects", menuName = "Scriptable Objects/Projects")]
public class Projects : ScriptableObject
{
    public enum typeOfProject : byte 
    {
        HyperCannon = 0,
        JumpBooster = 1,
        ShieldUpgrade = 2
    }
    [Tooltip("Change this value for match the right project")]
    public typeOfProject projecetType;
}
