using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class SimpleMovement : MonoBehaviour
{
    public InputActionAsset _IAA;
    public float gravity;

    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform playerCameraContainer;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private CharacterController _CC;
    private Vector2 readedMoveAxis;
    private Vector3 moveAxis;
    Animator animator;

    public bool isAttacking = false;

    public float rotationSpeed;

    private void OnEnable()
    {
        //Quando l`oggetto è attivo abilito il controllo degli input
        _IAA.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        //Se viene distrutto o disattivato il player smetto di track degli input Player
        _IAA.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _jumpAction = InputSystem.actions.FindAction("Jump");
        _CC = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    

    void HandleMovement()
    {
        readedMoveAxis = _moveAction.ReadValue<Vector2>();
        moveAxis = Vector2.ClampMagnitude(readedMoveAxis, 1f);

        Vector3 flatForward = Vector3.ProjectOnPlane(playerCameraContainer.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.ProjectOnPlane(playerCameraContainer.right, Vector3.up).normalized;

        Vector3 inputDir = flatForward * moveAxis.y + flatRight * moveAxis.x;
        
        if (inputDir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(inputDir), Time.deltaTime * rotationSpeed);

            // Attiva il layer della corsa (es. blend weight a 1)
            animator.SetLayerWeight(animator.GetLayerIndex("WalkLayer"), inputDir.sqrMagnitude);
        }
        else {
            // Disattiva o riduci il blend quando non ti muovi
            animator.SetLayerWeight(animator.GetLayerIndex("WalkLayer"), 0f);
        }

        float speedPowerupMultiplier = GameManager.playerStatic.GetComponent<PlayerState>().curSpeedMultiplier;

        Vector3 velocity = inputDir * movementSpeed * speedPowerupMultiplier + Vector3.up * gravity;
        _CC.Move(velocity * Time.deltaTime);
    }
    private void Update()
    {
        if (!isAttacking) HandleMovement();
    }
}
