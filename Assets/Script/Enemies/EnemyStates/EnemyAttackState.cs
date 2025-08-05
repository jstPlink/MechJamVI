using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private readonly Transform _target;

    public EnemyAttackState(Boss boss, Transform target) : base(boss)
    {
        _target = target;
    }

    public override void Enter()
    {
        boss.endAttackEvent += EndAttack;

        boss.GainEnergy();
        animator.SetTrigger(GetRandomAttack());
    }

    private string GetRandomAttack()
    {
        int randomIndex = Random.Range(0, boss.attacks.Count);
        return boss.attacks[randomIndex];
    }

    public void EndAttack()
    {
        boss.cooldownCoroutine = boss.StartCoroutine(WaitForCooldownRoutine());
        boss.ChangeState(new EnemyChaseState(boss, _target));
    }

    public IEnumerator WaitForCooldownRoutine()
    {
        float cooldown = Random.Range(boss.minAttackCooldown, boss.maxAttackCooldown);
        yield return new WaitForSeconds(cooldown);
        boss.cooldownCoroutine = null;
    }

    public override void Exit()
    {
        boss.endAttackEvent -= EndAttack;
    }
}
