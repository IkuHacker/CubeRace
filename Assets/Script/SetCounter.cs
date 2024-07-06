using UnityEngine;
using UnityEngine.UI;


public class SetCounter : MonoBehaviour
{
    public Text counterText;
    public PlayerMovement playerMovement;
    public ScoreManager scoreManager;



    public void SetText(string counter) 
    {
        counterText.text = counter;
    }

    public void SetGame()
    {

        playerMovement.enabled = true;
        scoreManager.enabled = true;
        scoreManager.UpdatingScore();

    }

    public void SetStart() 
    {
        Time.timeScale = 1f;
        playerMovement.enabled = false;
        scoreManager.enabled = false;
        scoreManager.isStart = false;
    }


   
}
