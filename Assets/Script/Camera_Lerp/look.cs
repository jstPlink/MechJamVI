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
        containerLookAxis = new Vector3(0, _lookAction.ReadValue<Vector2>().x, 0);
        cameraLookAxis = new Vector3(_lookAction.ReadValue<Vector2>().y, 0, 0);
        containerLookAxis.y = Mathf.Clamp(containerLookAxis.y, -1f, 1f);
        cameraLookAxis.x = Mathf.Clamp(cameraLookAxis.x, -1f, 1f);

        float yAngle = containerLookAxis.y * horizontalRotationSpeed * Time.deltaTime;
        float xAngle = cameraLookAxis.x * verticalRotationSpeed * Time.deltaTime;
        transform.RotateAround(target.position, Vector3.up, yAngle);
        float x = playerCamera.eulerAngles.x + xAngle;
        Debug.Log("Angle X : " + Mathf.Abs(normalizeRotation(x)));
        Debug.Log("xAngle : " + xAngle);
        Debug.Log("camera X : " + playerCamera.eulerAngles.x);
        if (Mathf.Abs(normalizeRotation(x)) < xDegree)
        {
            playerCamera.RotateAround(target.position, Vector3.right, xAngle);
        }
        
        
        //playerCamera.RotateAround(transform.position, cameraLookAxis, horizontalRotationSpeed * Time.deltaTime);





    }


    private float normalizeRotation(float angle)
    {
        float y = angle;
        if (y > 180)
        {
            y -= 360;
            Debug.Log(y + ": NORMALIZED");
        }
        return angle;
    }

    private void Update()
    {
        HandleLook();
    }
}
