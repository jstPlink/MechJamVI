using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public Animator anim;
    public Enemy enemy;


    public void UpdateHitState(int newState) {
        anim.SetInteger("hitState", newState);
    }

    public void Die()
    {
        enemy.Destroy();
    }


    public ParticleSystem dustVFX;

    public void PlayDust()
    {
        dustVFX.Play();
    }

}
