using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    GameObject player;

    // Main menu
    public GameObject settingsPanel;

    // Game
    public GameObject repairPanel;
    public GameObject powerupPanel;
    public GameObject blueprintPanel;

    // 
    public GameObject pwpPanel;
    public GameObject interactPanel;

    public AK.Wwise.Event _openMenu;
    public AK.Wwise.Event _closeMenu;
    public AK.Wwise.Event _startGame;

    private void Start()
    {
        ShowSettings(false);

        player = GameManager.playerStatic;
    }



    // GAME - PANELS
    public void OpenRepair()
    {
        repairPanel.SetActive(true);
        powerupPanel.SetActive(false);
        blueprintPanel.SetActive(false);
        // refresh repair
        repairPanel.GetComponent<PanelRepair>().RefreshPanel();
    }
    public void OpenPowerUps()
    {
        repairPanel.SetActive(false);
        powerupPanel.SetActive(true);
        blueprintPanel.SetActive(false);
        // refresh powerup
        powerupPanel.GetComponent<PanelPowerup>().RefreshPanels();
    }
    public void OpenBlueprints()
    {
        repairPanel.SetActive(false);
        powerupPanel.SetActive(false);
        blueprintPanel.SetActive(true);
        // refresh BP
    }


    
    // panel
    public void ShowPowerupPanel(bool showPanel)
    {
        if (showPanel)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _openMenu.Post(this.gameObject);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _closeMenu.Post(this.gameObject);
            Time.timeScale = 1;
        }

        pwpPanel.SetActive(showPanel);
    }
    public void ShowInteractionPrompt(bool show)
    {
        interactPanel.SetActive(show);
    }






    #region MainMenu
    public void StartGame()
    {
        // load level
        _startGame.Post(this.gameObject);
        SceneManager.LoadScene("S_Landscape");
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
