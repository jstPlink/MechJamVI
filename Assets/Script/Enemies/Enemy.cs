using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public Base_Behaviour _currentBase;
    public GameManager _gm;

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

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void CustomUpdate()
    {
        // print("updt");
    }

    public virtual void IncreseHealth()
    {
        if (GameManager._minute % 5 == 0) // se sono passati 5min
        {
            health *= _gm._healthMult;
        }
    }
}
