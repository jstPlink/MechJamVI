using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Minion : Enemy
{
    public float lifetime = 5f;
    private float timer = 0f;
    private bool isDying = false;
    GameObject player;

    Spawner mySpawner;
    Vector2 intervalRange = new Vector2(2f, 7f);



    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        agent.SetDestination(player.transform.position);


        StartCoroutine(TickLoop());
    }


    IEnumerator TickLoop()
    {
        while (true)
        {
            // Attesa con tempo casuale tra min e max
            float waitTime = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(waitTime);


            // LOGICA IA MINION


            if (agent.isOnNavMesh) agent.SetDestination(player.transform.position);


            // Il ciclo continua automaticamente con un nuovo timer
            StartCoroutine(TickLoop());
        }
    }




    public override void CustomUpdate()
    {
        base.CustomUpdate();

    }



    void Update()
    {
        timer += Time.deltaTime;

        if (!isDying && timer >= lifetime)
        {
            isDying = true;

            if (mySpawner) mySpawner.SetMinion();
            // Chiama la funzione prima della distruzione
            Destroy(gameObject);  // Distrugge il GameObject
        }
    }

    public void SetMySpawner(GameObject newSpawner)
    {
        mySpawner = newSpawner.GetComponent<Spawner>();
    }
}