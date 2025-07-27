using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header(" ## CONFIGURATIOn ##")]
    [Header(" -- Number Of Project --")]
    [Tooltip("Regulate value to match the right number of project in game")]
    [SerializeField] public bool[] unlockedProjects; //Follow numeration of Projects Enum

    public void UnlockProject(byte index)
    {
        unlockedProjects[index] = true;
    }

    public bool[] getProject()
    {
        return unlockedProjects;
    }
}
