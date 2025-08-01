using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class AttackSystem : MonoBehaviour
{
    InputAction attackAction;
    Animator animator;

    // ROBE COMBO
    bool canAttack = true;
    int comboStep = 0;

    public float firstAttackDuration = 1f;
    public float secondAttackDuration = 1f; // Quanto tempo ha il player per inserire il colpo successivo
    public float comboInputWindow = 0.7f; // Quanto tempo ha il player per inserire il colpo successivo



    private void Start() {
        animator = GetComponentInChildren<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }


    void Update() {
        if (canAttack) {
            if (attackAction.ReadValue<float>() > 0) {
                canAttack = false;
                comboStep++;
                SetComboState(comboStep);
            }
        }
    }


    public void SetGoState() {
        animator.SetBool("go", false);
    }

    // Viene chiamato dall'animazione per avvisare che puo attaccare di nuovo
    public void ResetAttackState()
    {   
        canAttack = true;
    }

    // Viene chiamato dal codice per aumentare la combo e passare ad un nuovo stato
    public void SetComboState(int newState) {

        comboStep = newState;
        animator.SetInteger("comboState", comboStep);
        
        // fai partire l'animazione
        animator.SetBool("go", true);
    }
}
