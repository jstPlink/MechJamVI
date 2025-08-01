using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class AttackSystem : MonoBehaviour
{
    InputAction attackAction;
    Animator animator;

    // ROBE COMBO
    public bool canAttack = true;
    public int comboStep = 0;




    private void Start() {
        animator = GetComponentInChildren<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }


    void Update() {
        if (canAttack) {
            if (attackAction.ReadValue<float>() > 0) {
                canAttack = false;
                IncremenetCombo();
            }
        }
    }



    // Viene chiamato dall'animazione per avvisare che puo attaccare di nuovo
    public void ResetAttackState() {   
        canAttack = true;
    }

    // Viene chiamato dal codice per aumentare la combo e passare ad un nuovo stato
    public void IncremenetCombo() {
        comboStep++;
        animator.SetInteger("comboStep", comboStep);
    }

    public void ResetCombo() {
        comboStep = 0;
        canAttack = true;
        animator.SetInteger("comboStep", comboStep);
    }
}
