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
        // Attesa con tempo casuale tra min e max
        float waitTime = Random.Range(intervalRange.x, intervalRange.y);
        yield return new WaitForSeconds(waitTime);

        // LOGICA IA MINION
        if (health > 0 && agent.isOnNavMesh && gameObject != null)
        {
            agent.SetDestination(GameManager.GetClosestBase(gameObject.transform));
            animator.SetFloat("speed", agent.velocity.magnitude);
        }
        else yield return null;

        // Il ciclo continua automaticamente con un nuovo timer
        StartCoroutine(TickLoop());
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

        agent.Stop();
        animator.SetBool("dead", true);
    }



    public void SetMySpawner(GameObject newSpawner) {
        mySpawner = newSpawner.GetComponent<Spawner>();
    }
}