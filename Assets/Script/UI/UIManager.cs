using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;


    private void Start()
    {
        ShowSettings(false);
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
