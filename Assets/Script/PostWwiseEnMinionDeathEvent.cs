using UnityEngine;

public class PostWwiseEnMinionDeathEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventEnMinionDeath;
    public void PlayWwiseEnMinionDeath()
    {
        AudioEventEnMinionDeath.Post(gameObject);
    }
}

