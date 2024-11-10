using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;

    public GameObject settingsWindow;
    public GameObject shopWindow;

    public GameObject skinPanel;
    public GameObject hatPanel;

    public Transform hatPanelTransform;
    public Transform skinPanelTransform;

    public GameObject buttonCanva;
    public Animator cameraAnimator;


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void TriggerCamera()
    {
        shopWindow.SetActive(false);
        buttonCanva.SetActive(false);
        cameraAnimator.SetTrigger("Start");
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
        cameraAnimator.SetBool("GoShop", true);
        buttonCanva.SetActive(false);
    }

    public void CloseShopWindow()
    {
        cameraAnimator.SetBool("GoShop", false);
        shopWindow.SetActive(false);
        buttonCanva.SetActive(true);


    }

    public void OpenHatPanel()
    {
        skinPanel.SetActive(false);
        hatPanel.SetActive(true);

        hatPanelTransform.SetAsLastSibling(); // Met le panneau des chapeaux au-dessus
        skinPanelTransform.SetAsFirstSibling(); // 

    }

    public void OpenSkinPanel()
    {
        skinPanel.SetActive(true);
        hatPanel.SetActive(false);

        skinPanelTransform.SetAsLastSibling(); // Met le panneau des skins au-dessus
        hatPanelTransform.SetAsFirstSibling();
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