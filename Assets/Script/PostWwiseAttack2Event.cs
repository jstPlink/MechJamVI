using UnityEngine;

public class PostWwiseAttack2Event : MonoBehaviour
{
    public AK.Wwise.Event AudioEventAtk2;
    public void PlayWwiseAttack2()
    {
        AudioEventAtk2.Post(gameObject);
    }
}
