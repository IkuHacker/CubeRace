using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadGame(int seed)
    {
        StateNameController.seeds = seed;

        SceneManager.LoadScene("MainEndlessScene");
    }

    public void LoadSizeLevel(float levelSize) 
    {
        StateNameController.levelSize = levelSize;
    }

    
}
