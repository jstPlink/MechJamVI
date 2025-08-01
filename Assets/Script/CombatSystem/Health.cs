using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;




    public virtual void ApplyDamage(float damageAmount)
    {
        health -= damageAmount;


        print("CurHealth: " + health);
        if (health <= 0) {
            OnDeath();
        }
    }

    public void OnDeath() {
        Destroy(gameObject);
    }
}
