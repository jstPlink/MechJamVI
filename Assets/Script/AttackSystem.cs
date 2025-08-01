using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class AttackSystem : MonoBehaviour
{
    InputAction attackAction;
    Animator animator;

    // ROBE COMBO
    public float comboResetTime = 1.0f; // Tempo max tra un attacco e l'altro
    public float attackDuration = 0.5f; // Durata di un attacco

    private enum AttackState { Idle, Attacking, WaitingForNextInput }
    private AttackState currentState = AttackState.Idle;

    private Queue<float> inputBuffer = new Queue<float>();
    private int comboStep = 0;
    private float lastInputTime;




    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        
        float isAttacking = attackAction.ReadValue<float>();
        animator.SetBool("attack1", isAttacking > 0 ? true : false);
        

        // Lettura input e buffering
        if (isAttacking > 0) // Sostituisci con l'input desiderato
        {
            if (inputBuffer.Count < 3)
                inputBuffer.Enqueue(Time.time);
        }

        switch (currentState)
        {
            case AttackState.Idle:
                if (inputBuffer.Count > 0)
                    StartCoroutine(HandleAttack());
                break;

            case AttackState.WaitingForNextInput:
                if (Time.time - lastInputTime > comboResetTime)
                {
                    ResetCombo();
                }
                else if (inputBuffer.Count > 0)
                {
                    StartCoroutine(HandleAttack());
                }
                break;
        }
    }


    IEnumerator HandleAttack()
    {
        currentState = AttackState.Attacking;
        float inputTime = inputBuffer.Dequeue();
        lastInputTime = Time.time;

        comboStep++;
        PlayAttackAnimation(comboStep);

        yield return new WaitForSeconds(attackDuration);

        if (comboStep >= 3)
        {
            ResetCombo();
        }
        else
        {
            currentState = AttackState.WaitingForNextInput;
        }
    }

    void ResetCombo()
    {
        comboStep = 0;
        inputBuffer.Clear();
        currentState = AttackState.Idle;
    }

    void PlayAttackAnimation(int step)
    {
        // Qui puoi chiamare l'animator o effetti
        switch (step)
        {
            case 1:
                Debug.Log("Attacco 1");
                break;
            case 2:
                Debug.Log("Attacco 2");
                break;
            case 3:
                Debug.Log("Attacco finale potente!");
                break;
        }
    }
}
