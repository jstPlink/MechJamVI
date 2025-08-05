using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly Transform _target;

    public EnemyChaseState(Boss boss, Transform target) : base(boss)
    {
        _target = target;
    }

    public override void Enter()
    {
        boss.playerOutOfRange += EndChase;

        animator.SetBool("IsWalking", true);
    }

    public override void Update()
    {
        base.Update();

        agent.SetDestination(_target.position);
        agent.isStopped = agent.remainingDistance <= boss.stopDistance;

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (agent.remainingDistance <= boss.attackDistance && !boss.isWaitingCooldown)
        {
            boss.ChangeState(new EnemyAttackState(boss, _target));
        }
    }

    private void EndChase(Transform target)
    {
        if (target == _target)
        {
            boss.ChangeState(new EnemyRetreatState(boss));
        }
    }

    public override void Exit()
    {
        boss.playerOutOfRange -= EndChase;

        animator.SetBool("IsWalking", false);

        agent.ResetPath();
    }
}
