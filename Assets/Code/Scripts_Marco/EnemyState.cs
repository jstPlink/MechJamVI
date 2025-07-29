using UnityEngine;

[System.Serializable]
public abstract class EnemyState
{
    public abstract void OnEnter(EnemyController _controller);
    public abstract void OnUpdate(EnemyController _controller);
    public abstract void OnExit(EnemyController _controller);
    public abstract void OnCollision(EnemyController _controller, Collider _collision);

    public abstract void DrawGizmo(EnemyController _controller);
}
