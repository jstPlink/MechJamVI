using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class ComboSystem : MonoBehaviour
{
    InputAction attackAction;
    Animator animator;

    // ROBE COMBO
    public bool canAttack = true;
    public int comboStep = 0;

    public SimpleMovement movement;


    private void Start() {
        animator = GetComponentInChildren<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }


    void Update() {
        if (canAttack) {
            if (attackAction.ReadValue<float>() > 0) {

                movement.isAttacking = true;
                canAttack = false;
                IncremenetCombo();
            }
        }
    }



    public void OnHit()
    {
        animator.SetTrigger("hit");
    }

    public void OnDeath()
    {
        animator.SetTrigger("isDead");
    }


    // Viene chiamato dall'animazione per avvisare che puo attaccare di nuovo
    public void ResetAttackState() {   
        canAttack = true;
    }

    // Viene chiamato dal codice per aumentare la combo e passare ad un nuovo stato
    public void IncremenetCombo() {
        comboStep++;
        animator.SetLayerWeight(2, 1);
        animator.SetInteger("comboStep", comboStep);
    }

    public void ResetCombo() {
        comboStep = 0;
        canAttack = true;
        movement.isAttacking = false;
        animator.SetLayerWeight(2, 0);
        animator.SetInteger("comboStep", comboStep);
    }
}
