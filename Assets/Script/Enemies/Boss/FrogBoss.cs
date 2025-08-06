using UnityEngine;

public class FrogBoss : Boss
{
    [Header("Charged attack")]
    [SerializeField] private Transform _barrel;
    [SerializeField] private Projectile _projectilePrefab;
    private Transform _target;

    public override void OnChargedAttack(Transform target)
    {
        _target = target;
        Vector3 targetDirection = (_target.position - _barrel.position).normalized;
        _barrel.rotation = Quaternion.LookRotation(Vector3.up, targetDirection);
    }

    public override void ActivateChargedAttack()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _barrel.position, _barrel.rotation);
        Vector3 targetDirection = (_target.position - _barrel.position).normalized;
        projectile.Shoot(targetDirection);
        _target = null;
    }
}
