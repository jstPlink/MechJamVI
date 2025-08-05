using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseState
{
    protected Animator animator => boss.animator;
    protected NavMeshAgent agent => boss.agent;
    protected readonly Boss boss;

    public EnemyBaseState(Boss boss)
    {
        this.boss = boss;
    }

    public abstract void Enter();
    public abstract void Exit();
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
