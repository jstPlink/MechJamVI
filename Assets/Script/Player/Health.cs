using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI textHealth;

    public void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;

        textHealth.SetText("HEALTH - " + health);

        if (health <= 0f)
        {
            Time.timeScale = 0f;
            Application.Quit();

        }
    }
}
