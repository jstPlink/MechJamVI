using UnityEngine;
using System.Collections;

public class Base_Behaviour : MonoBehaviour
{
    [Header(" ## Configuration ##")]
    [Header(" -- Pointer --")]
    private GameManager _gm;
    [Header(" -- Material --")]
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material allyMaterial;
    [Header(" -- Time --")]
    [Tooltip("Regulate the ammount of time needed for capture a base")]
    [SerializeField] private float timeForCapture;
    [SerializeField] private float timeLeftForCapture;
    [Tooltip("Regulate ammount of time to subtract from 'time left for capture' for enemies")]
    [SerializeField] private float timeToSubtractEnemyXTick;
    [Tooltip("Regulate ammount of time to subtract from 'time left for capture' for Player. Please note that every minion divide this value")]
    [SerializeField] private float timeToSubtractPlayerXTick;
    [Header(" -- Lights --")]
    [Tooltip("Pointer for the beacon`s lights. Top most is the first captured")]
    [SerializeField] private MeshRenderer[] Lights;
    [Header(" -- Resource --")]
    [Tooltip("Regulate the ammount of resource generated from this base")]
    [SerializeField] private float resourceGenAmmount;
    [SerializeField] private bool canGenerateResource;
    [Header(" -- Project Drop --")]
    [Tooltip("If enabled drop project")]
    [SerializeField] private bool haveProject;
    [Tooltip("Attach here the right Drop Variants")]
    [SerializeField] private GameObject dropPrefab;
    [Tooltip("Transform where spawn drops")]
    [SerializeField] private Transform spawnPointProject;
    [Tooltip("Drop push force")]
    [SerializeField] private float pulseForce;


    public enum Status : byte
    {
        Enemy = 0,
        Contested = 1,
        Ally = 2
    };
    [Header(" ## DEBUG ##")]
    [Header(" -- Status --")]
    [SerializeField] public Status _status;
    [SerializeField] private Status _owner;
    [SerializeField] private bool _isThereEnemy;
    [SerializeField] private byte enemyCounter = 0;
    [SerializeField] public bool _isTherePlayer;
    [SerializeField] private bool canModifyTime;
    [SerializeField] private float _timeXLight;

    private void Start()
    {
        timeLeftForCapture = timeForCapture;
        _timeXLight = timeForCapture / Lights.Length;

        _gm = FindAnyObjectByType<GameManager>();
    }

    public void ChangeStatus(Status newStatus)
    {
        _status = newStatus;
    }

    public Status GetStatus()
    {
        return _status;
    }

    public float GetResourceAmmount()
    {
        return resourceGenAmmount;
    }

    public bool CanGenerateResource()
    {
        return canGenerateResource;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Check se il player è nell`area della base
        {
            _isTherePlayer = true;
        }
        if (other.CompareTag("Enemy")) //Check se il nemico è nell`area della base
        {
            _isThereEnemy = true;
            enemyCounter++; //Aumento contatore nemici in base
        }

        if ((_status == Status.Enemy && other.CompareTag("Player")) || (_status == Status.Ally && other.CompareTag("Enemy")))
        //Se la base è nemica e il player entra o Se la base è alleata e un nemico entra allora cambio stato
        {
            ChangeStatus(Status.Contested);
            _gm.somethingChanged = true;
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
                _gm.somethingChanged = true;
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
        if (_status == Status.Contested)//Se la base è contestata
        {
            int lightNumber = (int)Mathf.Ceil(timeLeftForCapture / _timeXLight)-1;
            if (lightNumber < 0)
            {
                lightNumber = 0;
            }

            if (_isTherePlayer == false && _isThereEnemy == true)// Se non c'è il player, allora il nemico la può conquistare
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
                            _gm.somethingChanged = true;
                            timeLeftForCapture = timeForCapture;
                            canModifyTime = true;
                            canGenerateResource = false;
                            _gm.totalResourceXTick -= resourceGenAmmount;
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
            if (_isTherePlayer == true)// Se  c'è il player, allora può conquistare
            {
                float timeToSuttractPlayerWithEnemy = timeToSubtractPlayerXTick * (1f / (1f + enemyCounter)); //Il tempo è influenzato in % dal numero di nemici presenti
                //Debug.Log(timeToSuttractPlayerWithEnemy);
                if (canModifyTime)
                {
                    canModifyTime = false;
                    if (_owner == Status.Enemy)
                    {
                        if (timeLeftForCapture <= 0)
                        {
                            _owner = Status.Ally;
                            _status = Status.Ally;
                            _gm.somethingChanged = true;
                            timeLeftForCapture = timeForCapture;
                            canModifyTime = true;
                            canGenerateResource = true;
                            _gm.totalResourceXTick += resourceGenAmmount;
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
        yield return new WaitForSeconds(_gm.tickTimer);
        timeLeftForCapture -= timeToSubtract;
        canModifyTime = true;
    }
    private IEnumerator AddTime(float timeToAdd)
    {
        yield return new WaitForSeconds(_gm.tickTimer);
        timeLeftForCapture += timeToAdd;
        if (timeLeftForCapture > timeForCapture)
        {
            timeLeftForCapture = timeForCapture;
        }
        canModifyTime = true;
    }
}
