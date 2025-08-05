using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventMov;
    public void PlayMechastepSound()
    {
        AudioEventMov.Post(gameObject);
    }
}
