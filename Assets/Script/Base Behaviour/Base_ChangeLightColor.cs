using UnityEngine;
using System.Collections;

public class Base_ChangeLightColor : MonoBehaviour
{
    [Header(" ## Configuration ##")]
    [Header(" -- Material --")]
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Material allyMaterial;
    [Header(" -- Time --")]
    [SerializeField] private float timeForCapture;
    [SerializeField] private float timeLeftForCapture;
    [SerializeField] private float timeToSubtractEnemy;
    [SerializeField] private float timeToSubtractPlayer;
    [Header(" -- Lights --")]
    [SerializeField] private MeshRenderer[] Lights;
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
    [SerializeField] private byte enemyLights = 0;
    [SerializeField] private byte allyLights = 0;
    [SerializeField] private bool _isTherePlayer;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isTherePlayer = false;
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

            if (_isTherePlayer == false && _isThereEnemy == true)// Se non c'è il player, allora il nemico la può conquistare
            {
                if (_owner == Status.Ally)
                {
                    if (timeLeftForCapture <= 0)
                    {
                        _owner = Status.Enemy;
                        timeLeftForCapture = timeForCapture;
                    }
                    else
                    {
                        StartCoroutine(SubtractTime(timeToSubtractEnemy));
                    }
                }
                else
                {
                    StartCoroutine(AddTime(timeToSubtractEnemy));
                }
                Lights[lightNumber].material = enemyMaterial;
            }
            if (_isTherePlayer == true)// Se  c'è il player, allora può conquistare
            {
                float timeToSuttractPlayerWithEnemy = timeToSubtractPlayer * (1 / (1 + enemyCounter)); //Il tempo è influenzato in % dal numero di nemici presenti
                if (_owner == Status.Enemy)
                {
                    if (timeLeftForCapture <= 0)
                    {
                        _owner = Status.Ally;
                        timeLeftForCapture = timeForCapture;
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
                Lights[lightNumber].material = allyMaterial;
            }
        }
    }



    private IEnumerator SubtractTime(float timeToSubtract)
    {
        yield return new WaitForSeconds(timeToSubtract);
        timeLeftForCapture -= timeToSubtract;
    }
    private IEnumerator AddTime(float timeToAdd)
    {
        yield return new WaitForSeconds(timeToAdd);
        timeLeftForCapture += timeToAdd;
        if (timeLeftForCapture > timeForCapture)
        {
            timeLeftForCapture = timeForCapture;
        }
    }
}
