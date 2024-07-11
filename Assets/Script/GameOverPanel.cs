using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public bool isInfiniteLevel;
    public void RestartGame()
    {
        if (isInfiniteLevel) 
        {
            SceneManager.LoadScene("InfiniteLevel");
        }
        else 
        {
            SceneManager.LoadScene("MainEndlessScene");
        }
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
