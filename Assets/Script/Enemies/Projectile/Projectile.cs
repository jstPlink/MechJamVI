using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    new public Rigidbody rigidbody { get; private set; }
    new public SphereCollider collider { get; private set; }

    [Header("Projectile settings")]
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float despawnTime;

    private Coroutine despawnCoroutine;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    public void Shoot(Vector3 direction)
    {
        rigidbody.linearVelocity = direction * speed;
        despawnCoroutine = StartCoroutine(WaitForDespawn());
    }

    private IEnumerator WaitForDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.TryGetComponent(out Health health))
        // {
        //     health.ApplyDamage(damage);
        // }

        return;
        // DestroyProjectile();
    }
}
