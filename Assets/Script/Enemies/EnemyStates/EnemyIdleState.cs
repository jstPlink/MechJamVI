using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Boss boss) : base(boss) {}

    public override void Enter()
    {
        boss.endIdleEvent += EndIdle;
        boss.playerInRangeEvent += ChasePlayer;

        animator.SetTrigger(GetRandomIdle());
    }

    private string GetRandomIdle()
    {
        int randomIndex = Random.Range(0, 2);

        return randomIndex switch
        {
            0 => "idle1",
            1 => "idle2",
            _ => ""
        };
    }

    private void ChasePlayer(Transform target)
    {
        boss.ChangeState(new EnemyChaseState(boss, target));
    }

    private void EndIdle()
    {
        boss.ChangeState(new EnemyIdleState(boss));
    }

    public override void Exit()
    {
        boss.endIdleEvent -= EndIdle;
        boss.playerInRangeEvent -= ChasePlayer;
    }
}
