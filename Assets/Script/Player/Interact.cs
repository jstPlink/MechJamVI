using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{

    InputAction interaction;
    public UIManager uiManager;


    private void Start()
    {
        interaction = InputSystem.actions.FindAction("Interaction");
    }


    private void Update()
    {
        if (interaction.WasPerformedThisFrame())
        {
            uiManager.ShowPowerupPanel(true);

        }
    }
}
