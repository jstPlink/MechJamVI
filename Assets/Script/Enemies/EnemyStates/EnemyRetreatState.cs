using UnityEngine;

public class EnemyRetreatState : EnemyBaseState
{
    public EnemyRetreatState(Boss boss) : base(boss) {  }

    public override void Enter()
    {
        boss.playerInRangeEvent += ChasePlayer;

        animator.SetBool("IsWalking", true);

        agent.SetDestination(boss.patrolPosition);
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
    }

    public override void Update()
    {
        base.Update();

        if (agent.remainingDistance <= boss.stopDistance)
        {
            boss.ChangeState(new EnemyIdleState(boss));
        }
    }

    private void ChasePlayer(Transform target)
    {
        boss.ChangeState(new EnemyChaseState(boss, target));
    }

    public override void Exit()
    {
        boss.playerInRangeEvent -= ChasePlayer;

        animator.SetBool("IsWalking", false);
    }
}
