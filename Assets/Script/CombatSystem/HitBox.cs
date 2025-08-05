using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public float damage = 10f;
    
    private void OnTriggerEnter(Collider other) {

        // damage from player to enemy
        if (gameObject.tag == "PlayerHitbox" && other.tag == "Enemy") {
            float newDamage = damage * GameManager.playerStatic.GetComponent<PlayerState>().curAttackMultiplier;
            other.GetComponent<Enemy>().ApplyDamage(newDamage);
        }
        // damage fro enemy to player
        else if (gameObject.tag == "EnemyHitbox" && other.tag == "Player")
        {
            other.GetComponent<Health>().ApplyDamage(damage);
        }
    }
}
