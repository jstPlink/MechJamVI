using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    protected NavMeshAgent Agent { get; set; }

    protected EnemyState _currentState;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (_currentState != null) _currentState.OnUpdate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentState != null) _currentState.OnCollision(this, other);
    }

    public void SetState(EnemyState _state)
    {
        if (_state == null) return;

        if (_currentState != null)
            _currentState.OnExit(this);

        _currentState = _state;

        _currentState.OnEnter(this);
    }

    private void OnDrawGizmos()
    {
        if (_currentState != null) _currentState.DrawGizmo(this);
    }
}
