using UnityEngine;
using System.Collections;

namespace Old
{
    public class FrogBoss : Enemy
    {
        private float distance;
        public float chaseDistance;
        public float attackLDistance;
        public float fillOffset;
        public float energyFill;
        public float fillSpeed;
        public float fillPerAttack;
        public float projectileSpeed;
        [Tooltip("X = minimum time for next attack; Y = MAXIMUM time for next attack")]
        public Vector2 waitTime;
        private Animator _anim;
        [SerializeField] private Renderer rend;
        [SerializeField] private float prevFill;
        [SerializeField] private bool canAttack = true;
        [SerializeField] private Transform firePoint;
        private Transform player;
        public GameObject projectile;
        [Tooltip("Ref. to liquid material positon")]
        public byte index;

        //Need for Shader Animation
        private Vector2 fillLimit = new Vector2(0.82f, 3.4f);

        private void Awake()
        {
            //rend = GetComponent<Renderer>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _anim = GetComponentInChildren<Animator>();
            prevFill = rend.materials[index].GetFloat("_Fill");
            firePoint = GameObject.Find("Projectile_Spawn").GetComponent<Transform>();
        }

        private void Update()
        {
            //rend.material.SetFloat("_Fill", originalFill + fillOffset);
            prevFill = Mathf.Lerp(prevFill, energyFill, fillSpeed);
            rend.materials[index].SetFloat("_Fill", prevFill + fillOffset);
            // distance = Vector3.Distance(GameManager.GetPlayerPostion(), transform.position); //Distanza tra player e boss


            Debug.Log("Distance: " + distance);

            if (distance <= chaseDistance && distance > attackLDistance)
            {
                Debug.LogWarning("Distance less than delta");
                _anim.SetBool("isWalking", true);
            }
            else
            {
                _anim.SetBool("isWalking", false);

            }

            if (distance <= attackLDistance && canAttack)
            {
                if (energyFill < fillLimit.y)
                {
                    randomPickAttacks(2); //Se l'energia non � sufficente uso solo due attacchi
                }
                else
                {
                    randomPickAttacks(3);// Se ho abbastanza energia posso usare anche la cannonata
                }
            }
        }

        private void randomPickIdle()
        {
            int rnd = Random.Range(0, 2);
            if (rnd == 0)
            {
                //_anim.SetBool("idle1", false);
                _anim.SetTrigger("idle1");
            }
            else
            {
                //_anim.SetBool("idle1", true);
                _anim.SetTrigger("idle2");
            }
        }

        private void randomPickAttacks(int random)
        {
            canAttack = false;
            int rnd = Random.Range(0, random);
            switch (rnd)
            {
                case 0:
                    _anim.SetTrigger("Attack_L1");
                    energyFill += fillPerAttack;
                    break;
                case 1:
                    _anim.SetTrigger("Attack_L2");
                    energyFill += fillPerAttack;
                    break;
                case 2:
                    _anim.SetTrigger("Attack_H");
                    energyFill = fillLimit.x;
                    break;
            }
        }

        public void resetAttack()
        {
            StartCoroutine(ResetAndWait(Random.Range(waitTime.x, waitTime.y)));
        }

        private IEnumerator ResetAndWait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            canAttack = true;
        }

        public void heavyAttack()
        {
            firePoint.LookAt(player);
            Rigidbody rb = Instantiate(projectile, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(rb.transform.forward * projectileSpeed);
        }
    }
}
