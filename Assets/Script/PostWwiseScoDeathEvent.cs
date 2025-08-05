using UnityEngine;

public class PostWwiseScoDeathEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventScoDeath;
    public void PlayWwiseScoDeath()
    {
        AudioEventScoDeath.Post(gameObject);
    }
}
