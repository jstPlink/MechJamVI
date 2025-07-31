using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class look : MonoBehaviour
{
    public InputActionAsset _IAA;
    public float horizontalRotationSpeed;
    public float verticalRotationSpeed;
    private InputAction _lookAction;
    public Transform playerCamera;
    public Transform target;

    public float xDegree;

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
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    private void HandleLook()
    {
        float containerLookAxis = _lookAction.ReadValue<Vector2>().x;
        float cameraLookAxis = _lookAction.ReadValue<Vector2>().y;
        containerLookAxis = Mathf.Clamp(containerLookAxis, -1f, 1f);
        cameraLookAxis = Mathf.Clamp(cameraLookAxis, -1f, 1f);

        float yAngle = containerLookAxis * horizontalRotationSpeed * Time.deltaTime;
        float xAngle = cameraLookAxis * verticalRotationSpeed * Time.deltaTime;
        float x = playerCamera.eulerAngles.x + xAngle;

        transform.RotateAround(target.position, Vector3.up, yAngle);
        if (Mathf.Abs(normalizeRotation(x)) < xDegree)
        {
            playerCamera.RotateAround(target.position, playerCamera.right, xAngle);
        }
    }


    private float normalizeRotation(float angle)
    {
        float y = angle;
        if (y > 180)
        {
            y -= 360;
        }
        return y;
    }

    private void Update()
    {
        HandleLook();
    }
}
