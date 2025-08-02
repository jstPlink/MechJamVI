using UnityEngine;
using System;

public class HitBox : MonoBehaviour
{
    public float damage = 10f;
    
    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Enemy") {
            other.GetComponent<Enemy>().ApplyDamage(damage);
        }
    }
}
