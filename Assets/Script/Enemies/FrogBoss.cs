using UnityEngine;
using System.Collections;

public class FrogBoss : Enemy
{
    private float distance;
    public float chaseDistance;
    public float attackLDistance;
    public float fillOffset;
    [Tooltip("X = minimum time for next attack; Y = MAXIMUM time for next attack")]
    public Vector2 waitTime;
    private Animator _anim;
    [SerializeField] private  Renderer rend;
    [SerializeField] private  float originalFill;
    [SerializeField] private  bool canAttack = true;
    [Tooltip("Ref. to liquid material positon")]
    public byte index;

    private void Awake()
    {
        //rend = GetComponent<Renderer>();
        _anim = GetComponentInChildren <Animator>();
        originalFill = rend.materials[index].GetFloat("_Fill");
    }

    private void Update()
    {
        //rend.material.SetFloat("_Fill", originalFill + fillOffset);
        rend.materials[index].SetFloat("_Fill", originalFill + fillOffset);
        distance = Vector3.Distance(GameManager.GetPlayerPostion(),transform.position); //Distanza tra player e boss
       

            Debug.Log("Distance: " + distance);

        if (distance <= chaseDistance && distance > attackLDistance)
        {
            Debug.LogWarning("Distance less than delta");
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);

        }

        if (distance <= attackLDistance && canAttack)
        {
            randomPickAttacks();
        }
    }

    private void randomPickIdle()
    {
        int rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            //_anim.SetBool("idle1", false);
            _anim.SetTrigger("idle1");
        }
        else
        {
            //_anim.SetBool("idle1", true);
            _anim.SetTrigger("idle2");
        }
    }

    private void randomPickAttacks()
    {
        canAttack = false;
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                 _anim.SetTrigger("Attack_L1");
                break;
            case 1:
                _anim.SetTrigger("Attack_L2");
                break;
            case 2:
                _anim.SetTrigger("Attack_H");
                break;
        }
    }

    public void resetAttack()
    {
        StartCoroutine(ResetAndWait(Random.Range(waitTime.x,waitTime.y)));
    }

    private IEnumerator ResetAndWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }


}
