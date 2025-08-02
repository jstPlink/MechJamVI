using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Minion : Enemy
{
    Spawner mySpawner;
    public Vector2 intervalRange = new Vector2(2f, 7f);
    NavMeshAgent agent;
    Animator animator;

    NavMeshPath navMeshPath;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        navMeshPath = new NavMeshPath();
        StartCoroutine(TickLoop());
    }



    private void Update()
    {
        if (agent.velocity.sqrMagnitude <= 2f)
        {
            if (Vector3.Distance(transform.position, GameManager.player.transform.position) <= 15f) {
                animator.SetBool("canAttack", true);
            }
            else {
                animator.SetBool("canAttack", false);
            }
        }
    }



    Vector3 newDest = Vector3.zero;

    IEnumerator TickLoop()
    {
        while (health > 0)
        {
            if (agent.velocity.sqrMagnitude <= 2f) {
                yield return null;
            }


            newDest = Vector3.zero;
            newDest = GameManager.GetClosestBase(gameObject.transform);

            if (Vector3.Distance(transform.position, newDest) < 200f)
            {
                agent.SetDestination(newDest);
            }
            else
            {
                agent.Stop();
            }

            animator.SetFloat("speed", agent.velocity.magnitude)

            yield return new WaitForSeconds(Random.Range(intervalRange.x, intervalRange.y));
        }

        yield break;
    }


    public override void ApplyDamage(float damageAmount)
    {
        base.ApplyDamage(damageAmount);

        agent.Stop();
        if (health > 0) animator.SetInteger("hitState", 1);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        StopAllCoroutines();
        agent.Stop();
        animator.SetBool("dead", true);
    }



    public void SetMySpawner(GameObject newSpawner) {
        mySpawner = newSpawner.GetComponent<Spawner>();
    }
}