using UnityEngine;
using System.Collections;

public class Base_Behaviour : MonoBehaviour
{
    [Header(" ## Configuration ##")]
    [Header(" -- Material --")]
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material allyMaterial;
    [Header(" -- Time --")]
    [SerializeField] private float timeForCapture;
    [SerializeField] private float timeLeftForCapture;
    [SerializeField] private float timeToSubtractEnemyXTick;
    [SerializeField] private float timeToSubtractPlayerXTick;
    [SerializeField] private float tickTime;
    [Header(" -- Lights --")]
    [SerializeField] private MeshRenderer[] Lights;
    [Header(" -- Resource --")]
    [SerializeField] private float resourceGenAmmount;
    [SerializeField] private bool canGenerateResource;
    [Header(" -- Project Drop --")]
    [SerializeField] private bool haveProject;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform spawnPointProject;
    [SerializeField] private float pulseForce;


    public enum Status : byte
    {
        Enemy = 0,
        Contested = 1,
        Ally = 2
    };
    [Header(" ## DEBUG ##")]
    [Header(" -- Status --")]
    [SerializeField] private Status _status;
    [SerializeField] private Status _owner;
    [SerializeField] private bool _isThereEnemy;
    [SerializeField] private byte enemyCounter = 0;
    [SerializeField] private bool _isTherePlayer;
    [SerializeField] private bool canModifyTime;
    [SerializeField] private float _timeXLight;

    private void Start()
    {
        timeLeftForCapture = timeForCapture;
        _timeXLight = timeForCapture / Lights.Length;
    }

    public void ChangeStatus(Status newStatus)
    {
        _status = newStatus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Check se il player � nell`area della base
        {
            _isTherePlayer = true;
        }
        if (other.CompareTag("Enemy")) //Check se il nemico � nell`area della base
        {
            _isThereEnemy = true;
            enemyCounter++; //Aumento contatore nemici in base
        }

        if ((_status == Status.Enemy && other.CompareTag("Player")) || (_status == Status.Ally && other.CompareTag("Enemy")))
        //Se la base � nemica e il player entra o Se la base � alleata e un nemico entra allora cambio stato
        {
            ChangeStatus(Status.Contested);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTherePlayer = false;
            if (_isThereEnemy == true)
            {
                _status = Status.Contested;
            }
        }
        if (other.CompareTag("Enemy"))
        {
            enemyCounter--;
            if (enemyCounter == 0)
            {
                _isThereEnemy = false;
            }

        }
    }

    private void Update()
    {
        if (_status == Status.Contested)//Se la base � contestata
        {
            int lightNumber = (int)Mathf.Ceil(timeLeftForCapture / _timeXLight)-1;
            if (lightNumber < 0)
            {
                lightNumber = 0;
            }

            if (_isTherePlayer == false && _isThereEnemy == true)// Se non c'� il player, allora il nemico la pu� conquistare
            {
                if (canModifyTime)
                {
                    canModifyTime = false;
                    if (_owner == Status.Ally)
                    {
                        if (timeLeftForCapture <= 0)
                        {
                            _owner = Status.Enemy;
                            _status = Status.Enemy;
                            timeLeftForCapture = timeForCapture;
                            canModifyTime = true;
                            canGenerateResource = false;
                        }
                        else
                        {
                            StartCoroutine(SubtractTime(timeToSubtractEnemyXTick));
                        }
                    }
                    else
                    {
                        StartCoroutine(AddTime(timeToSubtractEnemyXTick));
                    }
                }
                    Lights[lightNumber].material = enemyMaterial;
            }
            if (_isTherePlayer == true)// Se  c'� il player, allora pu� conquistare
            {
                float timeToSuttractPlayerWithEnemy = timeToSubtractPlayerXTick * (1f / (1f + enemyCounter)); //Il tempo � influenzato in % dal numero di nemici presenti
                Debug.Log(timeToSuttractPlayerWithEnemy);
                if (canModifyTime)
                {
                    canModifyTime = false;
                    if (_owner == Status.Enemy)
                    {
                        if (timeLeftForCapture <= 0)
                        {
                            _owner = Status.Ally;
                            _status = Status.Ally;
                            timeLeftForCapture = timeForCapture;
                            canModifyTime = true;
                            canGenerateResource = true;
                            if (haveProject == true)
                            {
                                haveProject = false;
                                GameObject pj = Instantiate(dropPrefab, spawnPointProject.position, Quaternion.identity);
                                Rigidbody rb = pj.GetComponent<Rigidbody>();
                                rb.AddForce((Vector3.up + Vector3.back) * pulseForce, ForceMode.Impulse);
                            }
                        }
                        else
                        {
                            StartCoroutine(SubtractTime(timeToSuttractPlayerWithEnemy));
                        }
                    }
                    else
                    {
                        StartCoroutine(AddTime(timeToSuttractPlayerWithEnemy));
                    }
                }
                Lights[lightNumber].material = allyMaterial;
            }
        }
    }



    private IEnumerator SubtractTime(float timeToSubtract)
    {
        yield return new WaitForSeconds(tickTime);
        timeLeftForCapture -= timeToSubtract;
        canModifyTime = true;
    }
    private IEnumerator AddTime(float timeToAdd)
    {
        yield return new WaitForSeconds(tickTime);
        timeLeftForCapture += timeToAdd;
        if (timeLeftForCapture > timeForCapture)
        {
            timeLeftForCapture = timeForCapture;
        }
        canModifyTime = true;
    }
}
