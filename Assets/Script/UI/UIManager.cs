using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    // Main menu
    public GameObject settingsPanel;

    // Test map
    public GameObject map1;
    public GameObject map2;

    //private  controls;
    private InputAction _changemap;

    private void Start()
    {
        ShowSettings(false);

        _changemap = InputSystem.actions.FindAction("SwitchMap");
    }
    private void Update()
    {
        /*

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map1 && map2)
            {
                map1.SetActive(!map1.active);
                map2.SetActive(!map2.active);
            }
        }
        */
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
