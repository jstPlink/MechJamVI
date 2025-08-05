using UnityEngine;

public class PostWwiseScoDeathBEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventScoDeathB;
    public void PlayWwiseScoDeathB()
    {
        AudioEventScoDeathB.Post(gameObject);
    }
}
