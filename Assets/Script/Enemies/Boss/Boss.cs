using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Event DOC
 *
 * Every idle animation should call EndIdle
 * Light Attacks animation should call EndAttack
 * Charged Attacks animation should call StartChargedAttack, ActivateChargedAttack and EndAttack
 */

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Boss : Enemy
{
    // Public components
    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }

    // Fields
    [Header("Movement settings")]
    [SerializeField] private float _stopDistance;

    [Header("Visual setting")]
    [SerializeField] private Material liquidMat;
    [SerializeField] private float _liquidSpeed;
    [SerializeField] private float _liquidOffset;
    // [SerializeField] private float _disappearTime;
    private float _liquidFill;

    [Header("Combat settings")]
    [SerializeField] private float _attackDistance;
    [Min(0)]
    [SerializeField] private float _minAttackCooldown;
    [SerializeField] private float _maxAttackCooldown;

    [Header("Energy settings")]
    [Range(0, 1)]
    [SerializeField] private float _energyPercentageGaining;
    [Min(0)]
    [SerializeField] private float _minEnergy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _currentEnergy;

    private EnemyBaseState _currentState;

    [HideInInspector] public Vector3 patrolPosition;
    public Coroutine cooldownCoroutine;
    public List<string> attacks { get; private set; } = new() { "Attack_L1", "Attack_L2" };

    // Readonly properties
    public float minAttackCooldown => _minAttackCooldown;
    public float maxAttackCooldown => _maxAttackCooldown;
    public float attackDistance => _attackDistance;
    public float stopDistance => _stopDistance;
    public bool hasReachedMaxEnergy => _currentEnergy >= _maxEnergy;
    public bool isWaitingCooldown => cooldownCoroutine != null;

    // Events
    public event Action endIdleEvent;
    public event Action endAttackEvent;
    public event Action<Transform> playerInRangeEvent;
    public event Action<Transform> playerOutOfRangeEvent;
    public event Action chargedAttackEvent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        _currentEnergy = _minEnergy;
    }

    private void Start()
    {
        patrolPosition = transform.position;
        ChangeState(new EnemyIdleState(this));

        _liquidFill = liquidMat.GetFloat("_Fill");
    }

    private void Update()
    {
        _currentState.Update();

        _liquidFill = Mathf.MoveTowards(_liquidFill, _currentEnergy, _liquidSpeed * Time.deltaTime);
        liquidMat.SetFloat("_Fill", _liquidFill + _liquidOffset);
    }

    public void ChangeState(EnemyBaseState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public abstract void OnChargedAttack(Transform target);
    public abstract void ActivateChargedAttack();

    public void GainEnergy()
    {
        float gain = (_maxEnergy - _minEnergy) * _energyPercentageGaining;
        _currentEnergy = Mathf.Min(_currentEnergy + gain, _maxEnergy);

        if (hasReachedMaxEnergy && !attacks.Contains("Attack_H"))
        {
            attacks.Add("Attack_H");
        }
    }

    protected void ConsumeEnergy()
    {
        _currentEnergy = _minEnergy;

        attacks.Remove("Attack_H");
    }

    public void StartChargedAttack()
    {
        ConsumeEnergy();
        chargedAttackEvent?.Invoke();
    }

    public void EndAttack()
    {
        endAttackEvent?.Invoke();
    }

    public void EndIdle()
    {
        endIdleEvent?.Invoke();
    }

    public override void OnDeath()
    {
        ChangeState(new EnemyDeathState(this));
        enabled = false;
    }

    // NOTE - Se avete voglia potete fare che sparisca nel tempo
    // public override void Destroy()
    // {
    //     StartCoroutine(Desappear());
    // }

    // private IEnumerator Desappear()
    // {
    //     float elapsedTime = 0;

    //     while (elapsedTime < _disappearTime)
    //     {

    //         yield return null;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRangeEvent?.Invoke(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOutOfRangeEvent?.Invoke(other.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
