using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100f;
    [HideInInspector] public float maxHealth;
    public float shield = 100f;
    [HideInInspector] public float maxShield;
    public Slider barHealth;
    public Slider barShield;
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textShield;
    GameManager gm;

    public GameObject shieldMesh;


    InputAction shieldAction;

    private void Start()
    {
        maxHealth = health;
        maxShield = shield;
        shieldAction = InputSystem.actions.FindAction("Shield");
        gm = FindObjectOfType<GameManager>();

        UpdateUI();
    }

    private void Update()
    {
        if (shield > 0 && shieldAction.ReadValue<float>() > 0)
        {
            shieldMesh.SetActive(true);
        }
        else shieldMesh.SetActive(false);
    }


    public void BoostHealth(float multiplier)
    {
        maxHealth = maxHealth * multiplier;
        health = maxHealth;
        UpdateUI();
    }

    public void RestoreHealth(bool restoreHealth)
    {
        // ripristina vita
        if (restoreHealth) {
            if (!gm.RestoreHealth(health / maxHealth)) return;
            else health = maxHealth;
        }
        // ripristina scudo
        else if (!gm.RestoreHealth(-1)) return;
        else shield = maxShield;

        UpdateUI();
    }

    public void ApplyDamage(float damageAmount)
    {
        if (shieldAction.ReadValue<float>() > 0 && shield > 0) {
            shield -= damageAmount;
        } else health -= damageAmount;
        
        UpdateUI();

        if (health <= 0f)
        {
            Time.timeScale = 0f;
            Application.Quit();
        }
    }
    public void DamageShield(float damageAmount)
    {
        health -= damageAmount;
        UpdateUI();

        if (health <= 0f)
        {
            Time.timeScale = 0f;
            Application.Quit();

        }
    }

    
    void UpdateUI()
    {
        barHealth.SetValueWithoutNotify(health / maxHealth);
        barShield.SetValueWithoutNotify(shield / maxShield);
        textHealth.SetText(Mathf.RoundToInt(health).ToString());
        textShield.SetText(Mathf.RoundToInt(shield).ToString());
    }
}
