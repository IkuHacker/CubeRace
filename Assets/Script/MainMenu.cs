using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;
    public GameObject shopWindow;

    public GameObject skinPanel;
    public GameObject hatPanel;


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OpenSettingsButton()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }

    public void OpenShopButton()
    {
        shopWindow.SetActive(true);
    }

    public void CloseShopWindow()
    {
        shopWindow.SetActive(false);
    }

    public void OpenHatPanel()
    {
        skinPanel.SetActive(false);
        hatPanel.SetActive(true);

    }

    public void OpenSkinPanel()
    {
        skinPanel.SetActive(true);
        hatPanel.SetActive(false);
    }


    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}