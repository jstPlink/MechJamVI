using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // Main menu
    public GameObject settingsPanel;

    // Test map


    //private  controls;
    private InputAction _changemap;

    private void Start()
    {
        ShowSettings(false);

        _changemap = InputSystem.actions.FindAction("SwitchMap");
    }
    private void Update()
    {

    }

    


    #region MainMenu
    public void StartGame()
    {
        // load level
        SceneManager.LoadScene("S_MainLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    public void ShowSettings(bool show)
    {
        if (settingsPanel) settingsPanel.SetActive(show);
        else print("Settings panel not set");
    }
}
