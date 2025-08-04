using UnityEngine;

public class PostWwiseEnMinionHitEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventEnMinionHit;
    public void PlayWwiseEnMinionHit()
    {
        AudioEventEnMinionHit.Post(gameObject);
    }
}

