using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{

    public float interactionDistance = 20f;


    InputAction interaction;

    public UIManager interactionPanelScreen;
    public GameObject promptScene;

    private void Start()
    {
        interaction = InputSystem.actions.FindAction("Interaction");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptScene.SetActive(true);

            if (interaction.WasPerformedThisFrame())
            {
                interactionPanelScreen.ShowPowerupPanel(true);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            promptScene.SetActive(false);
        }



    }
}
