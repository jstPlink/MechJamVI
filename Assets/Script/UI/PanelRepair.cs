using UnityEngine;
using TMPro;

public class PanelRepair : MonoBehaviour
{

    public TextMeshProUGUI textCost;
    public bool isHealth = true;
    Health healthRef;
    public GameManager gm;

    public PanelRepair panelHealth;
    public PanelRepair panelShield;

    private void Awake()
    {
        healthRef = gm.player.GetComponent<Health>();

    }





    private void Update()
    {
        if (!textCost) UpdatePanels();
    }


    public void RefreshPanel()
    {
        if (healthRef == null) print("NOOOOOOOO");


        if (isHealth) {
            float perToRestore = 1 - (healthRef.health / healthRef.maxHealth);
            textCost.SetText((perToRestore * gm.maxHealthCost).ToString());
        }
        else {
            textCost.SetText((healthRef.shield != healthRef.maxShield ? gm.shieldRepairCost : 0).ToString());
        }
    }


    // GAME - FUNCTIONS
    public void RepairHull()
    {
        healthRef.RestoreHealth(true);
    }
    public void RepairShield()
    {
        healthRef.RestoreHealth(false);
    }




    public void UpdatePanels()
    {
        panelHealth.RefreshPanel();
        panelShield.RefreshPanel();
    }
}
