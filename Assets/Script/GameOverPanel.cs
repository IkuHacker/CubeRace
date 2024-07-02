using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public string levelToLoad;
    public void RestartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
