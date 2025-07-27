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
    [Tooltip("Match the number of the map with the pin")]
    [SerializeField] private MeshRenderer[] basesMapPin;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material allyMaterial;
    [SerializeField] private Material contestedMaterial;
    [Tooltip("UI for Resource Reference")]
    [SerializeField] private TextMeshProUGUI _resourceText;
    [Header(" ## DEBUG ##")]
    public float totalResourceXTick;
    [SerializeField] private float ResourceQty;
    [SerializeField] private bool canAdd = true;
    [SerializeField] public bool somethingChanged = true;

    private void Update()
    {
        if (canAdd)
        {
            canAdd = false;
            StartCoroutine(addResource());
        }
        if (somethingChanged)
        {
            somethingChanged = false;
            StartCoroutine(updateMap());
        }
    }

    private IEnumerator addResource()
    {
        ResourceQty += totalResourceXTick;
        _resourceText.text = "Resource: " + ResourceQty;
        yield return new WaitForSeconds(tickTimer);
        canAdd = true;
    }

    private IEnumerator updateMap()
    {
        for (byte i = 0; i < bases.Length; i++)
        {
            switch (bases[i].GetStatus())
            {
                case Base_Behaviour.Status.Ally:
                    basesMapPin[i].material = allyMaterial;
                    break;
                case Base_Behaviour.Status.Enemy:
                    basesMapPin[i].material = enemyMaterial;
                    break;
                case Base_Behaviour.Status.Contested:
                    basesMapPin[i].material = contestedMaterial;
                    break;
            }
        }
        yield return new WaitForSeconds(tickTimer);
    }
}
