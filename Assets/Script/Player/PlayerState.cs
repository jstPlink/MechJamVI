using UnityEngine;

public class PlayerState : MonoBehaviour
{



    public float curAttackMultiplier = 1f;
    public float curSpeedMultiplier = 1f;
    public float curHealthMultiplier = 1f;

    public int[] powerupCount = new int[] { 0, 0, 0 };



    public void UpdAttack(float amount) {
        curAttackMultiplier *= amount;
        powerupCount[0]++;
    }
    public void UpdHealth(float amount) {
        curHealthMultiplier *= amount;
        GetComponent<Health>().BoostHealth(amount);
        powerupCount[1]++;
    }
    public void UpdSpeed(float amount) {
        curSpeedMultiplier *= amount;
        powerupCount[2]++;
    }


}
