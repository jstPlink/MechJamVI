using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;

    public virtual void ApplyDamage(float damageAmount) {
        health -= damageAmount;

        if (health <= 0) {
            OnDeath();
        }
    }

    public virtual void OnDeath() {

    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void CustomUpdate()
    {
        // print("updt");
    }
}
