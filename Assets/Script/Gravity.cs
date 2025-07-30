using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = -9.81f;
    public float verticalVelocity = 0f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        if (controller == null) {
            // Debug.LogError("Nessun CharacterController trovato tra i figli!");
        }
    }

    void Update()
    {
        if (controller == null) return;

        if (!controller.isGrounded) {
            verticalVelocity += gravity * Time.deltaTime;
        }
        else if (verticalVelocity < 0) {
            verticalVelocity = -1f; // per restare "attaccati" al terreno
        }

        Vector3 move = new Vector3(0, verticalVelocity, 0);
        controller.Move(move * Time.deltaTime);
    }
}
