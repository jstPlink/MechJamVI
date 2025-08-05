using UnityEngine;

public class Vars : MonoBehaviour
{



    [System.Serializable]
    public struct Powerup
    {
        public string name;
        public string description;
        public float firstUpgradeMultiplier;
        public float repeatedUpgradeMultiplier;
        public float cost;
    }
}
