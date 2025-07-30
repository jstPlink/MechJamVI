using UnityEngine;
using UnityEngine.AI;

public class Minion : Enemy
{
    public float lifetime = 5f;
    private float timer = 0f;
    private bool isDying = false;

    Spawner mySpawner;


    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        agent.SetDestination(player.transform.position);
    }




    void Update()
    {
        timer += Time.deltaTime;

        if (!isDying && timer >= lifetime)
        {
            isDying = true;

            if(mySpawner) mySpawner.SetMinion();
            // Chiama la funzione prima della distruzione
            Destroy(gameObject);  // Distrugge il GameObject
        }
    }

    public void SetMySpawner(GameObject newSpawner)
    {
        mySpawner = newSpawner.GetComponent<Spawner>();
    }
}