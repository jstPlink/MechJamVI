using UnityEngine;

public class PostWwiseHAttackBEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventHAtkB;
    public void PlayWwiseHBAttack()
    {
        AudioEventHAtkB.Post(gameObject);
    }
}

