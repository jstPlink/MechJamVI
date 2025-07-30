using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Prefab e punto di spawn")]
    public GameObject minion;
    Minion myMinion;

    [Header("Tempi di spawn")]
    public Vector2 spawnTimeRange = new Vector2(2f,2f);
    public float spawnInterval = 3f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        // Attende un tempo casuale iniziale
        float initialDelay = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        yield return new WaitForSeconds(initialDelay);

        while (myMinion == null)
        {
            Spawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        GameObject newEnemy = Instantiate(minion, transform.position, transform.rotation);
        myMinion = newEnemy.GetComponent<Minion>();
        myMinion.SetMySpawner(transform.gameObject);   
    }

    public void SetMinion()
    {
        myMinion = null;
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }
}