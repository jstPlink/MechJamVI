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
    [SerializeField] private static Base_Behaviour[] bases;
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


    private void Start()
    {
        bases = FindObjectsByType<Base_Behaviour>(FindObjectsSortMode.None);

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


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
            // StartCoroutine(updateMap());
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


    public static Vector3 GetClosestBase(Transform enemyPosition)
    {
        Base_Behaviour closestBase = null;
        float thresholdDistance = Mathf.Infinity;

        foreach (Base_Behaviour tmpBase in bases)
        {
            if (tmpBase == null) continue; // per sicurezza

            if (tmpBase.GetStatus() == Base_Behaviour.Status.Contested || tmpBase.GetStatus() == Base_Behaviour.Status.Ally)
            {
                float distance = Vector3.Distance(enemyPosition.position, tmpBase.transform.position);
                if (distance < thresholdDistance)
                {
                    thresholdDistance = distance;
                    closestBase = tmpBase;
                }
            }
        }


        // Genera un punto in coordinate polari
        float angolo = Random.Range(0f, 2f * Mathf.PI);
        float distanza = Mathf.Sqrt(Random.Range(0f, 1f)) * 50f;

        float x = Mathf.Cos(angolo) * distanza;
        float z = Mathf.Sin(angolo) * distanza;

        return new Vector3(closestBase.transform.position.x + x, closestBase.transform.position.y, closestBase.transform.position.z + z);
    }
}
