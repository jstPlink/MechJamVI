public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Boss boss) : base(boss) {}

    public override void Enter()
    {
        animator.SetBool("isDeath", true);
    }

    public override void Exit() {}
}
