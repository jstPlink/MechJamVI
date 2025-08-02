using UnityEngine;

public class FrogBoss : Enemy
{
    private float distance;
    public float chaseDistance;
    public float fillOffset;
    private Animator _anim;
    [SerializeField] private  Renderer rend;
    [SerializeField] private  float originalFill;

    private void Awake()
    {
        //rend = GetComponent<Renderer>();
        _anim = GetComponentInChildren <Animator>();
        originalFill = rend.materials[1].GetFloat("_Fill");
    }

    private void Update()
    {
        //rend.material.SetFloat("_Fill", originalFill + fillOffset);
        rend.materials[1].SetFloat("_Fill", originalFill + fillOffset);
        distance = Vector3.Distance(GameManager.GetPlayerPostion(),transform.position); //Distanza tra player e boss
       

            Debug.Log("Distance: " + distance);

        if (distance <= chaseDistance)
        {
            Debug.LogWarning("Distance less than delta");
            _anim.SetBool("isWalking", true);
        }
        else
        {
            _anim.SetBool("isWalking", false);

        }
    }

    public void randomPickIdle()
    {
        float rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            _anim.SetBool("idle1", false);
        }
        else
        {
            _anim.SetBool("idle1", true);
        }
    }

}
