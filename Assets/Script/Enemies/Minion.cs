using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Minion : Enemy
{
    GameObject player;
    Spawner mySpawner;
    Vector2 intervalRange = new Vector2(2f, 7f);
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        agent.SetDestination(player.transform.position);
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(TickLoop());
    }


    IEnumerator TickLoop()
    {
        while (health > 0)
        {
            Vector3 newDestination = GameManager.GetClosestBase(gameObject.transform);
            agent.SetDestination(newDestination);
            animator.SetFloat("speed", agent.velocity.magnitude);

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