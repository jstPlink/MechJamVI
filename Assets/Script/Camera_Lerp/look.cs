using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class look : MonoBehaviour
{
    public InputActionAsset _IAA;
    public float horizontalRotationSpeed;
    public float verticalRotationSpeed;
    private InputAction _lookAction;
    private Vector3 containerLookAxis;
    private Vector3 cameraLookAxis;
    public Transform playerCamera;

    private void OnEnable()
    {
        //Quando l`oggetto � attivo abilito il controllo degli input
        _IAA.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        //Se viene distrutto o disattivato il player smetto di track degli input Player
        _IAA.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    private void HandleLook()
    {
        containerLookAxis = new Vector3(0, _lookAction.ReadValue<Vector2>().x, 0);
        cameraLookAxis = new Vector3(_lookAction.ReadValue<Vector2>().y, 0, 0);
        transform.Rotate(containerLookAxis * horizontalRotationSpeed * Time.deltaTime);
        playerCamera.Rotate(cameraLookAxis * verticalRotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        HandleLook();
    }
}
