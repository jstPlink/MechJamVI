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

    int comboStep = 1;

    public float firstAttackDuration = 1f;
    public float secondAttackDuration = 1f; // Quanto tempo ha il player per inserire il colpo successivo
    public float comboInputWindow = 0.7f; // Quanto tempo ha il player per inserire il colpo successivo



    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackAction = InputSystem.actions.FindAction("Attack");
    }


    void Update()
    {
        if (canAttack)
        {
            if (attackAction.ReadValue<float>() > 0)
            {
                print("CLICK" + comboStep);

                comboStep = comboStep < 4 ? comboStep++ : 0;
                canAttack = false;
                SetComboState(comboStep);
                StartCoroutine(WaitForAttack(comboStep == 1 ? firstAttackDuration : secondAttackDuration));
            }
        }
    }

    IEnumerator WaitForAttack(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        // riattiva input combo per breve tempo
        canAttack = true;

        /*
        float timer = 0f;
        while (timer < comboInputWindow)
        {
            timer += Time.deltaTime;
            // VERIFICA SE ARRIVANO INPUT PER CONTINUARE LA COMBO
            // if (attackAction.ReadValue<float>() > 0) break;

            yield return null;
        }
        */

        // REST COMBO STEP PERCHè NON CI SONO STATI INPUT?
        comboStep = 0;
    }

    void SetComboState(int curComboStep)
    {
        switch (curComboStep)
        {
            case 1:
                Debug.Log("Attacco 1");
                animator.SetTrigger("Attack1");
                break;
            case 2:
                Debug.Log("Attacco 2");
                animator.SetTrigger("Attack2");
                break;
            case 3:
                Debug.Log("Attacco finale");
                animator.SetTrigger("Attack3");
                break;
        }
    }

    public void EnableHitTrigger(int newState)
    {

        print("ASDRUBALE");
    }
}
