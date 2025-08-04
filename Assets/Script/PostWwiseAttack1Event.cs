using UnityEngine;

public class PostWwiseAttack1Event : MonoBehaviour
{
    public AK.Wwise.Event AudioEventAtk1;
    public void PlayWwiseAttack1()
    {
        AudioEventAtk1.Post(gameObject);
    }
}
