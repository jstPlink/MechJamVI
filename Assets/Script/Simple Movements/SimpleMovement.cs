using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class SimpleMovement : MonoBehaviour
{
    public InputActionAsset _IAA;
    public float gravity;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform playerCameraContainer;
    [SerializeField] private Transform playerCamera;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private CharacterController _CC;
    private Vector2 readedMoveAxis;
    private Vector3 moveAxis;

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
    }

    void HandleMovement()
    {
        readedMoveAxis = _moveAction.ReadValue<Vector2>() * movementSpeed;
        moveAxis = new Vector3(readedMoveAxis.x, 0, readedMoveAxis.y);
        // Debug.Log("MoveAxis: " + moveAxis);
        if (moveAxis != Vector3.zero)
        {
            transform.forward = playerCameraContainer.transform.forward;
        }
        Vector3 motion = moveAxis.z * transform.forward + (gravity*Vector3.up) + moveAxis.x * transform.right;
        _CC.Move(motion * Time.deltaTime);
    }

    private void Update()
    {
        HandleMovement();

    }
}
