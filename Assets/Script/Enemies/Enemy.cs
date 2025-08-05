using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Base_Behaviour _currentBase;

    private bool doOnce = true;

    public virtual void ApplyDamage(float damageAmount) {
        health -= damageAmount;

        if (health <= 0 && doOnce) {
            doOnce = false;
            OnDeath();
        }
    }

    public virtual void OnDeath() {
        _currentBase.minionDeath();
    }

    public virtual void AssignBase(Base_Behaviour newBase)
    {
        _currentBase = newBase;
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
