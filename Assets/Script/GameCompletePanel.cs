using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompletePanel : MonoBehaviour
{
    public string levelToLoad;
    public string nextLevelToLoad;
    public void RestartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void NextGame()
    {
        SceneManager.LoadScene(nextLevelToLoad);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

