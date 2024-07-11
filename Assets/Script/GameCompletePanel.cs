using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompletePanel : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainEndlessScene");
    }

    public void NextGame()
    {
        StateNameController.seeds++;
        SceneManager.LoadScene("MainEndlessScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

