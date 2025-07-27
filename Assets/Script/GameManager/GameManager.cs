using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header(" ## CONFIGURATIOn ##")]
    [Header(" -- Tick Timer (s) --")]
    [Tooltip("This value regulate how seconds pass for activate the next tick")]
    public float tickTimer;
    [Header(" -- Pointer --")]
    [Tooltip("Attach here every base of the game")]
    [SerializeField] private Base_Behaviour[] bases;
    [Tooltip("UI for Resource Reference")]
    [SerializeField] private TextMeshProUGUI _resourceText;
    [Header(" ## DEBUG ##")]
    public float totalResourceXTick;
    [SerializeField] private float ResourceQty;
    [SerializeField] private bool canAdd = true;

    private void Update()
    {
        if (canAdd)
        {
            canAdd = false;
            StartCoroutine(addResource());
        }
    }

    private IEnumerator addResource()
    {
        ResourceQty += totalResourceXTick;
        _resourceText.text = "Resource: " + ResourceQty;
        yield return new WaitForSeconds(tickTimer);
        canAdd = true;
    }
}
