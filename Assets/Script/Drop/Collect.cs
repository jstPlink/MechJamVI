using UnityEngine;

public class Collect : MonoBehaviour
{
    [Header(" ## CONFIGURATIOn ##")]
    [Header(" -- Pointer --")]
    [Tooltip("Attach here GameObject where inventory is attached to")]
    [SerializeField] private Inventory _inventory;
    [Tooltip("Attach here ScriptableGameojcet of the right Project")]
    public Projects _projectToCollect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory.UnlockProject((byte)_projectToCollect.projecetType);
            Destroy(this.gameObject);
        }
    }
}
