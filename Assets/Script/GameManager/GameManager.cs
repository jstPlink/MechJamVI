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

    public static GameObject player;


    private void Start()
    {
        bases = FindObjectsByType<Base_Behaviour>(FindObjectsSortMode.None);

        player = FindFirstObjectByType<SimpleMovement>().gameObject;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;
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
        if (_resourceText != null) _resourceText.text = "Resource: " + ResourceQty;
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

        // cerca tra le basi il punto piu vicino
        foreach (Base_Behaviour tmpBase in bases)
        {
            // Verifica che sia contestata o di un alleato
            if (tmpBase._status == Base_Behaviour.Status.Contested || tmpBase._status == Base_Behaviour.Status.Ally)
            {
                if (tmpBase._isTherePlayer) return player.transform.position;

                float distance = Vector3.Distance(enemyPosition.position, tmpBase.transform.position);
                if (distance < thresholdDistance)
                {
                    thresholdDistance = distance;
                    closestBase = tmpBase;
                }
            }
        }
        if (closestBase == null) return player.transform.position;

        // Genera un punto casuale all'interno della base
        Vector2 point = Random.insideUnitCircle * 20f;
        Vector3 targetLoc = new Vector3(point.x, 0f, point.y) + closestBase.transform.position;
        return targetLoc;
    }
}
