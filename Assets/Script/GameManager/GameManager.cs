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
    public TextMeshProUGUI textResourcesUI;


    [Header(" -- REPAIRS --")]
    public float maxHealthCost = 10f;
    public float shieldRepairCost = 350f;

    [Header(" -- POWERUPS --")]
    public float powerupCostIncrement = 1.5f;
    [SerializeField] public Vars.Powerup attackPowerup;
    [SerializeField] public Vars.Powerup healthPowerup;
    [SerializeField] public Vars.Powerup speedPowerup;


    [Header(" ## DEBUG ##")]
    public float totalResourceXTick;
    [SerializeField] public float ResourceQty;
    [SerializeField] private bool canAdd = true;
    [SerializeField] public bool somethingChanged = true;

    public GameObject player;
    public static GameObject playerStatic;





    private void Start()
    {
        bases = FindObjectsByType<Base_Behaviour>(FindObjectsSortMode.None);

        player = FindFirstObjectByType<SimpleMovement>().gameObject;
        playerStatic = player;

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Confined;
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

    

    public bool CheckResourcesAvailability(int resourcesRequest)
    {
        if (resourcesRequest <= ResourceQty)
        {
            ResourceQty -= resourcesRequest;
            return true;
        }
        else
        {
            print("NO RESOURCES");
        }
        return false;
    }

    private IEnumerator addResource()
    {
        ResourceQty += totalResourceXTick;
        if (_resourceText != null)
        {
            _resourceText.SetText(ResourceQty.ToString());
            textResourcesUI.SetText(ResourceQty.ToString());
        }
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
                // SE C'è IL PLAYER IN ZONA VALLO A PICCHIARE
                if (tmpBase._isTherePlayer) return playerStatic.transform.position;

                float distance = Vector3.Distance(enemyPosition.position, tmpBase.transform.position);
                if (distance < thresholdDistance)
                {
                    thresholdDistance = distance;
                    closestBase = tmpBase;
                }
            }
        }
        if (closestBase == null) return playerStatic.transform.position;

        // Genera un punto casuale all'interno della base
        Vector2 point = Random.insideUnitCircle * 20f;
        Vector3 targetLoc = new Vector3(point.x, 0f, point.y) + closestBase.transform.position;
        return targetLoc;
    }


    public bool RestoreHealth(float percToRestore)
    {
        if (percToRestore == -1f)
        { // shield
            if (ResourceQty >= shieldRepairCost) {
                ResourceQty -= shieldRepairCost;
                return true;
            }
            else return false;
        }
        else
        { // health
            if (ResourceQty >= maxHealthCost * percToRestore)
            {
                ResourceQty -= maxHealthCost * percToRestore;
                return true;
            }
            else return false;
        }
    }
}
