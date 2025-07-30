using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{


    // Intervallo minimo e massimo tra un tick e l'altro
    Vector2 intervalRange = new Vector2(4f, 8f);

    // Lista di script trovati nei figli
    private List<Spawner> spawners = new List<Spawner>();

    void Start()
    {
        // Cerca tra tutti i figli lo script specifico "MyAIScript" e lo raccoglie in lista
        foreach (Transform child in transform)
        {
            Spawner ai = child.GetComponent<Spawner>();
            if (ai != null)
            {
                spawners.Add(ai);
            }
        }

        // Avvia il primo timer con durata casuale
        // StartCoroutine(TickLoop());
    }

    IEnumerator TickLoop()
    {
        while (true)
        {
            // Attesa con tempo casuale tra min e max
            float waitTime = Random.Range(intervalRange.x, intervalRange.y);
            yield return new WaitForSeconds(waitTime);

            // Cicla tutti gli agent e chiama la loro funzione "EvaluateAI"
            foreach (Spawner spawner in spawners)
            {
                if (spawner != null) {
                    if (spawner.myMinion) {
                        spawner.myMinion.CustomUpdate();
                    }
                }
            }

            // Il ciclo continua automaticamente con un nuovo timer
            StartCoroutine(TickLoop());
        }
    }
}
