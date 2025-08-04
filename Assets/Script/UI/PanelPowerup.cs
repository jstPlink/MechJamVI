using UnityEngine;
using TMPro;

public class PanelPowerup : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDescription;
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI textUpgrades;

    public PanelPowerup panelAttack;
    public PanelPowerup panelHealth;
    public PanelPowerup panelSpeed;

    GameManager gm;
    Vars.Powerup curStats = default;


    public int panelType = 0;
    float cost;


    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();

        if (panelType != -1) RefreshPanel();
    }

    public void RefreshPanel()
    {

        switch (panelType)
        {
            case 0:
                curStats = gm.attackPowerup;
                break;

            case 1:
                curStats = gm.healthPowerup;
                break;

            case 2:
                curStats = gm.speedPowerup;
                break;

            default:
                Debug.LogWarning("Valore non previsto!");
                break;
        }

        textName.text = curStats.name;
        textDescription.text = curStats.description;

        int[] updatesCount = gm.player.GetComponent<PlayerState>().powerupCount;
        int totUpgrades = updatesCount[0] + updatesCount[1] + updatesCount[2];
        cost = (curStats.cost * Mathf.Clamp((float)totUpgrades, 1f, 999f));
        textCost.text = Mathf.RoundToInt(cost).ToString();
        textUpgrades.text = "Active upgrades: " + gm.player.GetComponent<PlayerState>().powerupCount[panelType].ToString();
    }

    public void RefreshPanels()
    {
        panelAttack.RefreshPanel();
        panelHealth.RefreshPanel();
        panelSpeed.RefreshPanel();
    }


    // UPGRADES
    public void UpdAttack()
    {
        if (!gm.CheckResourcesAvailability((int)cost)) return;

        gm.player.GetComponent<PlayerState>().UpdAttack(cost == curStats.cost ? curStats.firstUpgradeMultiplier : curStats.repeatedUpgradeMultiplier);
        RefreshPanel();
    }
    public void UpdHealth()
    {
        if (!gm.CheckResourcesAvailability((int)cost)) return;

        gm.player.GetComponent<PlayerState>().UpdHealth(cost == curStats.cost ? curStats.firstUpgradeMultiplier : curStats.repeatedUpgradeMultiplier);
        RefreshPanel();
    }
    public void UpdSpeed()
    {
        if (!gm.CheckResourcesAvailability((int)cost)) return;

        gm.player.GetComponent<PlayerState>().UpdSpeed(cost == curStats.cost ? curStats.firstUpgradeMultiplier : curStats.repeatedUpgradeMultiplier);
        RefreshPanel();
    }
}
