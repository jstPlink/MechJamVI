using UnityEngine;

public class PostWwiseHAttackEvent : MonoBehaviour
{
    public AK.Wwise.Event AudioEventHAtk;
    public void PlayWwiseHAttack()
    {
        AudioEventHAtk.Post(gameObject);
    }
}

