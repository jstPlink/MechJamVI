using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public float damage = 10f;
    
    private void OnTriggerEnter(Collider other) {

        if (gameObject.tag == "PlayerHitbox" && other.tag == "Enemy") {
            other.GetComponent<Enemy>().ApplyDamage(damage);
        }
        else if (gameObject.tag == "EnemyHitbox" && other.tag == "Player")
        {
            other.GetComponent<Health>().ApplyDamage(damage);
        }
    }
}
