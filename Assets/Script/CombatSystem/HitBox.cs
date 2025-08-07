using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public float damage = 10f;
    public ParticleSystem[] hitEffect;
    
    private void OnTriggerEnter(Collider other) {

        // damage from player to enemy
        if (gameObject.tag == "PlayerHitbox" && other.tag == "Enemy") {
            float newDamage = damage * GameManager.playerStatic.GetComponent<PlayerState>().curAttackMultiplier;
            other.GetComponent<Enemy>().ApplyDamage(newDamage);
            GameManager.PlayCameraShake();
        }
        // damage fro enemy to player
        else if (gameObject.tag == "EnemyHitbox" && other.tag == "Player")
        {
            if (hitEffect.Length > 0) {
                foreach (ParticleSystem ps in hitEffect)
                {
                    ps.Play();
                }
            }
            
            other.GetComponent<Health>().ApplyDamage(damage);
        }
    }
}
