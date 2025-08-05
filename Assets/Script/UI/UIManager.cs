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


    

    

    // BLUEPRINTS
    public void AddBP1()
    {

    }
    public void AddBP2()
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
